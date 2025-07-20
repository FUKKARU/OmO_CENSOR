namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
        private static bool Check_NoEvenNumber(this string text)
        {
            int i = 0;
            while (i < text.Length)
            {
                if (char.IsDigit(text[i]))
                {
                    int start = i;
                    while (i < text.Length && char.IsDigit(text[i])) i++;

                    string numberStr = text.Substring(start, i - start);
                    numberStr = ConvertToHalfWidth(numberStr);

                    if (int.TryParse(numberStr, out int number))
                    {
                        if (number % 2 == 0) return false;
                    }
                }
                else
                {
                    i++;
                }
            }

            return true;
        }

        private static string ConvertToHalfWidth(string input)
        {
            var sb = new System.Text.StringBuilder(input.Length);
            foreach (char c in input)
            {
                if (c >= '０' && c <= '９')
                {
                    sb.Append((char)(c - '０' + '0'));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}