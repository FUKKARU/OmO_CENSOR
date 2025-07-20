using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class Manual : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;



    [Header("切り替え候補の文章一覧")]
    [TextArea]
    [SerializeField] private List<string> messages = new List<string>();

    /// <summary>
    /// ボタンから呼び出してランダムな文章を表示
    /// </summary>
    public void ShowRandomMessage()
    {
        if (messages.Count == 0)
        {
            Debug.LogWarning("文章リストが空です！");
            return;
        }

        int index = Random.Range(0, messages.Count);
        string selected = messages[index];

        targetText.text = selected;
        Debug.Log($"ランダム文章切り替え: {selected}");
    }


    public float CalculateKanjiAccuracy(string fullText, string detectedText)
    {
        // 元文章の漢字カウント
        Dictionary<char, int> originalCount = new Dictionary<char, int>();
        foreach (char c in fullText)
        {
            if (IsKanji(c))
            {
                if (!originalCount.ContainsKey(c)) originalCount[c] = 0;
                originalCount[c]++;
            }
        }

        // 検閲済みテキストの漢字カウント
        Dictionary<char, int> detectedCount = new Dictionary<char, int>();
        foreach (char c in detectedText)
        {
            if (IsKanji(c))
            {
                if (!detectedCount.ContainsKey(c)) detectedCount[c] = 0;
                detectedCount[c]++;
            }
        }

        // 正しく一致した個数を計算（重複考慮）
        int matchCount = 0;
        int totalRequired = 0;
        foreach (var kvp in originalCount)
        {
            totalRequired += kvp.Value;
            int found = detectedCount.ContainsKey(kvp.Key) ? detectedCount[kvp.Key] : 0;
            matchCount += Mathf.Min(found, kvp.Value);
        }

        float accuracy = totalRequired == 0 ? 1f : (float)matchCount / totalRequired;
        Debug.Log($"正答率：{accuracy * 100:0.0}%（{matchCount}/{totalRequired}）");
        return accuracy;
    }

    // CJK漢字か判定
    private bool IsKanji(char c)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), @"\p{IsCJKUnifiedIdeographs}");
    }



}
