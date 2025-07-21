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
            // 1文の中に句読点が3つ以上含まれていたらアウト
            // 1文は、「。」「！」「？」「!」「?」のいずれかで囲まれている部分

            for (int i = 0; i < text.Length; i++)
            {
                char begin = text[i];
                if (begin is not ('。' or '！' or '？' or '!' or '?'))
                    continue; // 文の始まりではない

                for (int j = i + 1; j < text.Length; j++)
                {
                    char end = text[j];
                    if (end is not ('。' or '！' or '？' or '!' or '?'))
                        continue; // 文の終わりではない

                    // 文が見つかった
                    string sentence = text.Substring(i, j - i + 1);
                    int cnt = CountTouten(sentence);

                    if (cnt >= 3)
                        return false; // 句読点が多すぎる
                    break; // 次の文を探す
                }
            }

            return true; // 句読点が少ないか、文が見つからなかった



            // 「、」「,」の数をカウントする
            static int CountTouten(string text)
            {
                int count = 0;
                foreach (char c in text)
                {
                    if (c is ('、' or ','))
                        count++;
                }
                return count;
            }
        }
    }
}