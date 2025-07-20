using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;




public class Manual : MonoBehaviour
{
    [Header("対象のTextMeshPro UI")]
    [SerializeField] private TMP_Text targetText;


    public List<char> ExtractKanjiFromTMP()
    {
        string input = targetText.text;
        List<char> kanjiList = new List<char>();

        foreach (char c in input)
        {
            if (Regex.IsMatch(c.ToString(), @"\p{IsCJKUnifiedIdeographs}"))
            {
                kanjiList.Add(c);
                Debug.Log($"漢字発見：{c}");
            }
        }

        if (kanjiList.Count == 0)
        {
            Debug.Log("漢字は見つかりませんでした！");
        }
        else
        {
            Debug.Log($"漢字抽出完了：{kanjiList.Count}文字");
        }

        return kanjiList;
    }


}
