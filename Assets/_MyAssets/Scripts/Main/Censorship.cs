using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Censorship : MonoBehaviour
{
    [Header("Text & Drawing")]
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float thresholdRadius = 0.1f;

    private List<Rect> characterBounds = new();
    private List<int> characterRealIndices = new(); // 実テキストのインデックス
    private List<CensoredRange> censoredRanges = new();
    private HashSet<int> censoredIndices = new();  // 実テキストインデックスの記録

    private List<LineRenderer> drawnLines = new();
    private LineRenderer currentLine;
    private List<Vector3> currentLinePoints = new();

    private const float fixedZ = 90f;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        CacheCharacterBounds();
    }

    void Update()
    {
        HandleDrawingInput();
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
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = fixedZ;

            if (currentLinePoints.Count == 0 || Vector3.Distance(mouseWorldPos, currentLinePoints[^1]) > 0.01f)
            {
                currentLinePoints.Add(mouseWorldPos);
                currentLine.positionCount = currentLinePoints.Count;
                currentLine.SetPosition(currentLinePoints.Count - 1, mouseWorldPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckLineCollision(currentLinePoints);
        }
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
            CheckLineSegmentAgainstCharacters(start, end);
        }
    }

    private void CheckLineSegmentAgainstCharacters(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        float len = dir.magnitude;
        if (len == 0f) return;

        Vector3 norm = dir / len;
        int steps = Mathf.CeilToInt(len / 0.01f);

        for (int step = 0; step <= steps; step++)
        {
            Vector3 point = start + norm * (len * step / steps);
            point.z = fixedZ;

            for (int i = 0; i < characterBounds.Count; i++)
            {
                Vector3 center = new Vector3(characterBounds[i].center.x, characterBounds[i].center.y, fixedZ);
                if (Vector3.Distance(center, point) < thresholdRadius)
                {
                    int realIndex = characterRealIndices[i];
                    AddToCensoredList(realIndex);
                }
            }
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

        censoredRanges.Sort((a, b) => a.startIndex.CompareTo(b.startIndex));
        string result = string.Concat(censoredRanges.ConvertAll(r => r.originalWord));
        Debug.Log($"塗りつぶされた文字: 「{result}」");

        CacheCharacterBounds(); // 再取得（位置ずれ対応）
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
