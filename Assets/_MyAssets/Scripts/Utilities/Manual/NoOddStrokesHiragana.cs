namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
        private static readonly string OddStrokesHiragana = "あおかくけさしせそつてにのはひへまむもやをんがぐげざじぜぞづでばびべぷぽ";

        private static bool Check_NoOddStrokesHiragana(this string text)
        {
            foreach (char c in text)
            {
                foreach (char oddHiragana in OddStrokesHiragana)
                {
                    if (c == oddHiragana)
                        return false;
                }
            }
            return true;
        }
    }
}