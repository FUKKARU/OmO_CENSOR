using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Censorship : MonoBehaviour
{
    [Header("Text & Drawing")]
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private LineRenderer lineRendererPrefab; // プレハブ
    [SerializeField] private Camera mainCamera;

    private List<Rect> characterBounds = new();
    private List<CensoredRange> censoredRanges = new();

    private List<LineRenderer> drawnLines = new();        // すべての線
    private LineRenderer currentLine;                     // 今描いてる線
    private List<Vector3> currentLinePoints = new();      // 今の線の点列

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        CacheCharacterBounds();
    }

    private void Update()
    {
        HandleDrawingInput();
    }

    private void HandleDrawingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 新しい線を生成
            currentLine = Instantiate(lineRendererPrefab, transform);
            currentLine.positionCount = 0;
            currentLinePoints.Clear();
            drawnLines.Add(currentLine);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

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
        TMP_TextInfo textInfo = targetText.textInfo;

        characterBounds.Clear();

        Canvas canvas = targetText.canvas;
        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            Vector3 bl = charInfo.bottomLeft;
            Vector3 tr = charInfo.topRight;

            // ローカル→スクリーン→ワールド変換
            Vector3 screenBL = RectTransformUtility.WorldToScreenPoint(cam, targetText.transform.TransformPoint(bl));
            Vector3 screenTR = RectTransformUtility.WorldToScreenPoint(cam, targetText.transform.TransformPoint(tr));

            Vector3 worldBL = mainCamera.ScreenToWorldPoint(new Vector3(screenBL.x, screenBL.y, mainCamera.nearClipPlane));
            Vector3 worldTR = mainCamera.ScreenToWorldPoint(new Vector3(screenTR.x, screenTR.y, mainCamera.nearClipPlane));

            Rect rect = new Rect(worldBL, worldTR - worldBL);
            characterBounds.Add(rect);
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
        for (int i = 0; i < characterBounds.Count; i++)
        {
            if (LineIntersectsRect(start, end, characterBounds[i]))
            {
                AddToCensoredList(i, targetText.text[i].ToString());
            }
        }
    }

    private void AddToCensoredList(int index, string word)
    {
        if (censoredRanges.Exists(r => r.startIndex == index)) return;

        censoredRanges.Add(new CensoredRange(index, 1, word));
        ApplyCensorship();
    }

    private void ApplyCensorship()
    {
        // インデックス順に並べる
        censoredRanges.Sort((a, b) => a.startIndex.CompareTo(b.startIndex));

        List<string> censoredWords = new();
        foreach (var range in censoredRanges)
        {
            censoredWords.Add($"\"{range.originalWord}\" at index {range.startIndex}");
        }

        CacheCharacterBounds(); // 再取得
        Debug.Log("Censored characters (sorted): " + string.Join(", ", censoredWords));
    }

    private bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rect rect)
    {
        Vector2 topLeft = new(rect.xMin, rect.yMax);
        Vector2 topRight = new(rect.xMax, rect.yMax);
        Vector2 bottomLeft = new(rect.xMin, rect.yMin);
        Vector2 bottomRight = new(rect.xMax, rect.yMin);

        return
            LinesIntersect(p1, p2, topLeft, topRight) ||
            LinesIntersect(p1, p2, topRight, bottomRight) ||
            LinesIntersect(p1, p2, bottomRight, bottomLeft) ||
            LinesIntersect(p1, p2, bottomLeft, topLeft);
    }

    private bool LinesIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
    {
        float d = (a2.x - a1.x) * (b2.y - b1.y) - (a2.y - a1.y) * (b2.x - b1.x);
        if (Mathf.Approximately(d, 0)) return false;

        float u = ((b1.x - a1.x) * (b2.y - b1.y) - (b1.y - a1.y) * (b2.x - b1.x)) / d;
        float v = ((b1.x - a1.x) * (a2.y - a1.y) - (b1.y - a1.y) * (a2.x - a1.x)) / d;

        return (u >= 0 && u <= 1) && (v >= 0 && v <= 1);
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
