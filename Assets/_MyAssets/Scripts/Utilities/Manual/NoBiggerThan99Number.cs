namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
        private static bool Check_NoBiggerThan99Number(this string text)
        {
            // 数字の連続を抽出する正規表現
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(text, @"\d+");

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                if (int.TryParse(match.Value, out int number))
                {
                    if (number > 99)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}