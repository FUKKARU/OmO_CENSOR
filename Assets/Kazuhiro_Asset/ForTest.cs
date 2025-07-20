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
        string detected = detectedString; // åüâ{Ç≈èEÇ¡ÇΩï∂éö

        float accuracy = scanner.CalculateKanjiAccuracy(fullText, detected);
        Debug.Log($"é¿å±åãâ ÅFê≥ìöó¶ÇÕ {accuracy * 100:0.0}%");

    }

}
