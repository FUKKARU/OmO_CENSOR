using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class ForTest : MonoBehaviour
{
    [SerializeField] Manual scanner;

    public void OnCheckButton()
    {
        List<char> foundKanji = scanner.ExtractKanjiFromTMP();
        // �K�v�Ȃ珈���֓n���EUI�ɔ��f�Ȃ�
    }

}
