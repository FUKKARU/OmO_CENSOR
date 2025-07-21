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
    public string Get(int index) => index switch
    {
        1 => m1,
        2 => m2,
        3 => m3,
        4 => m4,
        5 => m5,
        _ => string.Empty,
    };
}
