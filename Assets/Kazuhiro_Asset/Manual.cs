using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Manual : MonoBehaviour
{
    [Header("�Ώۂ�TextMeshPro UI")]
    [SerializeField] private TMP_Text targetText;

    public float CalculateKanjiAccuracy(string fullText, string detectedText)
    {
        // �����͂̊����J�E���g
        Dictionary<char, int> originalCount = new Dictionary<char, int>();
        foreach (char c in fullText)
        {
            if (IsKanji(c))
            {
                if (!originalCount.ContainsKey(c)) originalCount[c] = 0;
                originalCount[c]++;
            }
        }

        // ���{�ς݃e�L�X�g�̊����J�E���g
        Dictionary<char, int> detectedCount = new Dictionary<char, int>();
        foreach (char c in detectedText)
        {
            if (IsKanji(c))
            {
                if (!detectedCount.ContainsKey(c)) detectedCount[c] = 0;
                detectedCount[c]++;
            }
        }

        // ��������v���������v�Z�i�d���l���j
        int matchCount = 0;
        int totalRequired = 0;
        foreach (var kvp in originalCount)
        {
            totalRequired += kvp.Value;
            int found = detectedCount.ContainsKey(kvp.Key) ? detectedCount[kvp.Key] : 0;
            matchCount += Mathf.Min(found, kvp.Value);
        }

        float accuracy = totalRequired == 0 ? 1f : (float)matchCount / totalRequired;
        Debug.Log($"�������F{accuracy * 100:0.0}%�i{matchCount}/{totalRequired}�j");
        return accuracy;
    }

    // CJK����������
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
    //            Debug.Log($"���������F{c}");
    //        }
    //    }

    //    if (kanjiList.Count == 0)
    //    {
    //        Debug.Log("�����͌�����܂���ł����I");
    //    }
    //    else
    //    {
    //        Debug.Log($"�������o�����F{kanjiList.Count}����");
    //    }

    //    return kanjiList;
    //}



}
