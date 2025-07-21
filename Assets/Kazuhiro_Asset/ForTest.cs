using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class ForTest : MonoBehaviour
{
    [SerializeField] KanjiBanRule scanner;
    [SerializeField] TMP_Text manualText;
    [SerializeField] string detectedString;
    void Start()
    {
        //List<char> foundKanji = scanner.ExtractKanjiFromTMP();
        string fullText = manualText.text;
        string detected = detectedString; // ???{??E????????

        float accuracy = scanner.CalculateKanjiAccuracy(fullText, detected);
        Debug.Log($"????????F???????? {accuracy * 100:0.0}%");

    }

}
