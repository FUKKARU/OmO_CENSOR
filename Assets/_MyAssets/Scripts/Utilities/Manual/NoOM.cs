namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
        private static bool Check_NoOM(this string text)
        {
            // 禁止文字のパターン（例: o, m, お, も, む など）
            // 必要に応じて文字を追加してください
            // 正規表現で禁止文字が含まれるか判定
            return !System.Text.RegularExpressions.Regex.IsMatch(text, "[omおもむまみめ]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}