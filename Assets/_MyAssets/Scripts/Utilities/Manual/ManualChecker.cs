namespace Scripts.Utilities
{
    public enum ManualId : byte
    {
        NoKanji = 0, // 漢字禁止
        NoMuchKutoten = 1, // 1分の中に3つ以上の句読点禁止
        NoContinuousHiragana = 2, // 5文字以上連続するひらがな禁止
        NoEvenNumber = 3, // 偶数禁止
        NoOddStrokesHiragana = 4, // 奇数画数のひらがな禁止
        NoSymbol = 5, // 記号禁止

        // 以下、禁忌
        NoBiggerThan99Number = 16, // 99より大きい数字禁止
        NoOM = 17, // oとｍ、及びそれらを発音に用いるかな文字禁止
    }

    public static partial class ManualChecker
    {
        // textに対して指定されたManualIdのチェックを行う(複数)
        public static bool Check(this string text, ManualId[] ids)
        {
            if (string.IsNullOrEmpty(text)) return true;
            if (ids == null || ids.Length == 0) return true;

            foreach (ManualId id in ids)
            {
                $"Checking {id} in text: {text}. Result is : {text.Check(id)}".Log();
                if (text.Check(id) == false)
                    return false;
            }
            return true;
        }

        // textに対して指定されたManualIdのチェックを行う(単一)
        private static bool Check(this string text, ManualId id) => id switch
        {
            ManualId.NoKanji => text.Check_NoKanji(),
            ManualId.NoMuchKutoten => text.Check_NoMuchKutoten(),
            ManualId.NoContinuousHiragana => text.Check_NoContinuousHiragana(),
            ManualId.NoEvenNumber => text.Check_NoEvenNumber(),
            ManualId.NoOddStrokesHiragana => text.Check_NoOddStrokesHiragana(),
            ManualId.NoSymbol => text.Check_NoSymbol(),
            ManualId.NoBiggerThan99Number => text.Check_NoBiggerThan99Number(),
            ManualId.NoOM => text.Check_NoOM(),
            _ => true,
        };
    }
}