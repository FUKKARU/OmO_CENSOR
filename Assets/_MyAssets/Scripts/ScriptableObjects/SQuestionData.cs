using UnityEngine;
using Scripts.Utilities;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SQuestionData", menuName = "ScriptableObjects/SQuestionData")]
    public sealed class SQuestionData : AResourcesScriptableObject<SQuestionData>
    {
        [SerializeField, TextArea(1, 1000)] private string q1;
        [SerializeField, TextArea(1, 1000)] private string q2;
        [SerializeField, TextArea(1, 1000)] private string q3;
        [SerializeField, TextArea(1, 1000)] private string q4;
        [SerializeField, TextArea(1, 1000)] private string q5;

        // indexは1始まりで！
        public string Get(int index) => index switch
        {
            1 => q1,
            2 => q2,
            3 => q3,
            4 => q4,
            5 => q5,
            _ => string.Empty,
        };
    }
}