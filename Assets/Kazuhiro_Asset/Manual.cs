using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;




public class Manual : MonoBehaviour
{
    [Header("�Ώۂ�TextMeshPro UI")]
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
                Debug.Log($"���������F{c}");
            }
        }

        if (kanjiList.Count == 0)
        {
            Debug.Log("�����͌�����܂���ł����I");
        }
        else
        {
            Debug.Log($"�������o�����F{kanjiList.Count}����");
        }

        return kanjiList;
    }


}
