using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.ScriptableObjects;
using Scripts.Scenes.Result;
using UnityEngine.UI.Extensions;

public class Censorship : MonoBehaviour
{
    [Header("Text & Drawing")]
    [SerializeField] private TextMeshProUGUI targetText;

    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float thresholdRadius = 0.1f;
    [SerializeField] private float maxInk = 100f;
    [SerializeField] private float ink = 100f;
    [SerializeField] private float useInk = 0.1f;
    [SerializeField] private Image fillInk;
    [SerializeField] private Vector2 screenDrawMin = new Vector2(100, 100);
    [SerializeField] private Vector2 screenDrawMax = new Vector2(1000, 800);
    [SerializeField] private TextMeshProUGUI bookTextA;
    [SerializeField] private TextMeshProUGUI bookTextB;
    [SerializeField] private TextMeshProUGUI pageText;

    private List<Rect> characterBounds = new();
    private List<int> characterRealIndices = new(); // 実テキストのインデックス
    private List<CensoredRange> censoredRanges = new();
    private HashSet<int> censoredIndices = new();  // 実テキストインデックスの記録

    private List<LineRenderer> drawnLines = new();
    private LineRenderer currentLine;
    private List<Vector3> currentLinePoints = new();

    private const float fixedZ = 0;

    // 残された文字
    string remainingText = string.Empty;
    public string RemainingText => remainingText;

    // 検閲された文字
    string censoredChars;
    public string CensoredChars => censoredChars;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        CacheCharacterBounds();
        SetTargetText();
    }

    void Update()
    {
        HandleDrawingInput();
    }


    private void SetTargetText()
    {
        targetText.text = SQuestionData.Entity.Get(ResultState.WhenClearNowLevel);
    }

    private void HandleDrawingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentLine = Instantiate(lineRendererPrefab, transform);
            currentLine.positionCount = 0;
            currentLinePoints.Clear();
            drawnLines.Add(currentLine);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 screenPos = Input.mousePosition;

            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(screenPos);
            mouseWorldPos.z = fixedZ;

            if (currentLinePoints.Count == 0 || Vector3.Distance(mouseWorldPos, currentLinePoints[^1]) > 0.01f)
            {
                float distance = 0f;
                if (currentLinePoints.Count > 0)
                {
                    distance = Vector3.Distance(mouseWorldPos, currentLinePoints[^1]);
                }

                float inkToUse = distance * useInk;
                if (ink >= inkToUse)
                {
                    ink -= inkToUse;

                    currentLinePoints.Add(mouseWorldPos);
                    currentLine.positionCount = currentLinePoints.Count;
                    currentLine.SetPosition(currentLinePoints.Count - 1, mouseWorldPos);
                }
                else
                {
                    Debug.Log("インクが足りません");
                }
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            CheckLineCollision(currentLinePoints);
        }

        fillInk.fillAmount = ink / maxInk;
    }

    private void CacheCharacterBounds()
    {
        targetText.ForceMeshUpdate();
        var textInfo = targetText.textInfo;

        characterBounds.Clear();
        characterRealIndices.Clear();

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            Vector3 bl = targetText.transform.TransformPoint(charInfo.bottomLeft);
            Vector3 tr = targetText.transform.TransformPoint(charInfo.topRight);
            bl.z = tr.z = fixedZ;

            characterBounds.Add(new Rect(bl, tr - bl));
            characterRealIndices.Add(charInfo.index); // テキスト内の実インデックスを保存
        }
    }

    private void CheckLineCollision(List<Vector3> linePoints)
    {
        for (int i = 0; i < linePoints.Count - 1; i++)
        {
            Vector3 start = linePoints[i];
            Vector3 end = linePoints[i + 1];
            CheckLineSegmentAgainstCharacters(start, end, lineRendererPrefab.startWidth);
        }
    }

    private void CheckLineSegmentAgainstCharacters(Vector3 start, Vector3 end, float width)
    {
        Vector2 start2D = new Vector2(start.x, start.y);
        Vector2 end2D = new Vector2(end.x, end.y);
        float radius = width * 0.5f;

        for (int i = 0; i < characterBounds.Count; i++)
        {
            Rect rect = characterBounds[i];

            if (ThickLineIntersectsRect(start2D, end2D, radius, rect))
            {
                int realIndex = characterRealIndices[i];
                AddToCensoredList(realIndex);
            }
        }

        // 線分が幅を持っている場合の、帯（カプセル）と矩形の交差判定
        static bool ThickLineIntersectsRect(Vector2 a, Vector2 b, float radius, Rect rect)
        {
            // ① 中心線と拡張された矩形の交差
            Rect expandedRect = new Rect(rect.xMin - radius, rect.yMin - radius,
                                         rect.width + radius * 2, rect.height + radius * 2);
            if (LineIntersectsRect(a, b, expandedRect)) return true;

            // ② 線分の端点が矩形の中にある（線が内部で終わるパターン）
            if (rect.Contains(a) || rect.Contains(b)) return true;

            // ③ 線の端点が、矩形の端に半径以内で近接（接触）
            Vector2 closestA = ClosestPointOnRect(rect, a);
            Vector2 closestB = ClosestPointOnRect(rect, b);
            if ((closestA - a).sqrMagnitude <= radius * radius) return true;
            if ((closestB - b).sqrMagnitude <= radius * radius) return true;

            return false;
        }

        // 線分と矩形の交差（細い線）
        static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rect r)
        {
            Vector2 tl = new Vector2(r.xMin, r.yMax);
            Vector2 tr = new Vector2(r.xMax, r.yMax);
            Vector2 bl = new Vector2(r.xMin, r.yMin);
            Vector2 br = new Vector2(r.xMax, r.yMin);

            return LineSegmentsIntersect(p1, p2, tl, tr) ||
                   LineSegmentsIntersect(p1, p2, tr, br) ||
                   LineSegmentsIntersect(p1, p2, br, bl) ||
                   LineSegmentsIntersect(p1, p2, bl, tl) ||
                   r.Contains(p1) || r.Contains(p2);
        }

        // 線分同士の交差判定
        static bool LineSegmentsIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            float d = (a2.x - a1.x) * (b2.y - b1.y) - (a2.y - a1.y) * (b2.x - b1.x);
            if (Mathf.Approximately(d, 0f)) return false; // 平行

            float u = ((b1.x - a1.x) * (b2.y - b1.y) - (b1.y - a1.y) * (b2.x - b1.x)) / d;
            float v = ((b1.x - a1.x) * (a2.y - a1.y) - (b1.y - a1.y) * (a2.x - a1.x)) / d;

            return (u >= 0f && u <= 1f) && (v >= 0f && v <= 1f);
        }

        // 点aに最も近いrect内の点
        static Vector2 ClosestPointOnRect(Rect rect, Vector2 a)
        {
            float x = Mathf.Clamp(a.x, rect.xMin, rect.xMax);
            float y = Mathf.Clamp(a.y, rect.yMin, rect.yMax);
            return new Vector2(x, y);
        }
    }

    private void AddToCensoredList(int realIndex)
    {
        if (censoredIndices.Contains(realIndex)) return;

        string ch = targetText.text[realIndex].ToString();
        censoredRanges.Add(new CensoredRange(realIndex, 1, ch));
        censoredIndices.Add(realIndex);

        ApplyCensorship();
    }

    private void ApplyCensorship()
    {
        if (censoredRanges.Count == 0)
        {
            Debug.Log("現在、検閲された文字はありません。");
            return;
        }

        // ソートして順序保持
        censoredRanges.Sort((a, b) => a.startIndex.CompareTo(b.startIndex));

        // 検閲された文字列
        censoredChars = string.Concat(censoredRanges.ConvertAll(r => r.originalWord));

        // 残された文字列
        string originalText = targetText.text;
        char[] remainingChars = originalText.ToCharArray();
        foreach (var r in censoredRanges)
        {
            for (int i = r.startIndex; i < r.startIndex + r.length; i++)
            {
                if (i >= 0 && i < remainingChars.Length)
                    remainingChars[i] = '■'; // マスキング記号
            }
        }

        remainingText = new string(remainingChars);
        // targetText.text = remainingText;

        Debug.Log($"■ 検閲された文字: 「{censoredChars}」");
        Debug.Log($"■ 残された文字列: 「{remainingText}」");

        CacheCharacterBounds(); // 再取得
    }

    private void OnDrawGizmos()
    {
        if (characterBounds == null || characterBounds.Count == 0) return;

        for (int i = 0; i < characterBounds.Count; i++)
        {
            Vector3 center = characterBounds[i].center;
            center.z = fixedZ;
            Gizmos.color = censoredIndices.Contains(characterRealIndices[i]) ? new Color(1, 0, 0, 0.4f) : new Color(0, 1, 0, 0.2f);
            Gizmos.DrawSphere(center, thresholdRadius);
        }
    }
}

[Serializable]
public class CensoredRange
{
    public int startIndex;
    public int length;
    public string originalWord;

    public CensoredRange(int startIndex, int length, string originalWord)
    {
        this.startIndex = startIndex;
        this.length = length;
        this.originalWord = originalWord;
    }
}
