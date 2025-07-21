namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
        private static bool Check_NoContinuousHiragana(this string text)
        {
            int count = 0;

            foreach (char c in text)
            {
                if (IsHiragana(c))
                {
                    count++;
                    if (count >= 5) return false;
                }
                else count = 0;
            }

            return true;
        }

        private static bool IsHiragana(char c)
        {
            return c >= '\u3040' && c <= '\u309F';
        }
    }
}