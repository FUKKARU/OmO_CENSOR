namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
#if false
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Test_Check_MochKutoten()
        {
            string test1 = "これはテストです。問題ありません。";
            string test2 = "これは、テストです、問題が、あるようです。";

            UnityEngine.Debug.Log($"test1（句読点少）: {test1.Check_NoMuchKutoten()}"); // → true
            UnityEngine.Debug.Log($"test2（句読点多）: {test2.Check_NoMuchKutoten()}"); // → false

        }
#endif

        public static bool Check_NoMuchKutoten(this string text)
        {
            int count = 0;

            foreach (char c in text)
            {
                if (c == '、' || c == '。')
                {
                    count++;
                    if (count >= 3) return false; // 句読点が3つ以上 → 違反
                }
            }

            return true; // 句読点が2つ以下 → 許容
        }


    }
}