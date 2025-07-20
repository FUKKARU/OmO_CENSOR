using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Manual : MonoBehaviour
{
    [Header("対象のTextMeshPro UI")]
    [SerializeField] private TMP_Text targetText;

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

    //public List<char> ExtractKanjiFromTMP()
    //{
    //    string input = targetText.text;
    //    List<char> kanjiList = new List<char>();

    //    foreach (char c in input)
    //    {
    //        if (Regex.IsMatch(c.ToString(), @"\p{IsCJKUnifiedIdeographs}"))
    //        {
    //            kanjiList.Add(c);
    //            Debug.Log($"漢字発見：{c}");
    //        }
    //    }

    //    if (kanjiList.Count == 0)
    //    {
    //        Debug.Log("漢字は見つかりませんでした！");
    //    }
    //    else
    //    {
    //        Debug.Log($"漢字抽出完了：{kanjiList.Count}文字");
    //    }

    //    return kanjiList;
    //}



}
