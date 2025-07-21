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
        {
            return !System.Text.RegularExpressions.Regex.IsMatch(text, @"\p{IsCJKUnifiedIdeographs}");
        }
    }
}
