namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
        private static bool Check_NoBiggerThan99Number(this string text)
        {
            var matches = System.Text.RegularExpressions.Regex.Matches(text, @"[\d０-９]+");

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string numberStr = ConvertToHalfWidth(match.Value);

                if (int.TryParse(numberStr, out int number) && number > 99)
                {
                    return false;
                }
            }

            return true;
        }
    }
}