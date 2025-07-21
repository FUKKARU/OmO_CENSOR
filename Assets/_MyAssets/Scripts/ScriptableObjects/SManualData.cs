using Scripts.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "SManualData", menuName = "ScriptableObjects/SManualData")]
public class SManualData : AResourcesScriptableObject<SManualData>
{

    [SerializeField, TextArea(1, 1000)] private string m1;
    [SerializeField, TextArea(1, 1000)] private string m2;
    [SerializeField, TextArea(1, 1000)] private string m3;
    [SerializeField, TextArea(1, 1000)] private string m4;
    [SerializeField, TextArea(1, 1000)] private string m5;

    // indexは1始まりで！
    // 改行毎に区切る
    public string[] Get(int index)
    {
        string rawData = index switch
        {
            1 => m1,
            2 => m2,
            3 => m3,
            4 => m4,
            5 => m5,
            _ => string.Empty,
        };

        if (string.IsNullOrEmpty(rawData))
            return null;

        // 改行で分割して配列にする
        string[] lines = rawData.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lines.Length; i++)
            lines[i] = lines[i].Trim();
        return lines;
    }
}
