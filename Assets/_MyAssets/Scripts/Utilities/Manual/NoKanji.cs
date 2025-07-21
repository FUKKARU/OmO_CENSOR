using System.Text.RegularExpressions;

namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
#if false
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Test_Check_NoKanji()
        {
            UnityEngine.Debug.Log("Hello World");
        }
#endif
        public static bool Check_NoKanji(this string text)
            => Regex.IsMatch(text, @"[\u4E00-\u9FFF\u3400-\u4DBF]");
    }
}
