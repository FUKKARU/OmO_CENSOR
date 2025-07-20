using System.Text.RegularExpressions;

namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {
#if false // テスト用
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Test_Check_NoSymbol()
        {
            // true
            UnityEngine.Debug.Log($"あいうえお : {"あいうえお".Check_NoSymbol()}"); // 全角ひらがな
            UnityEngine.Debug.Log($"ｱｲｳｴｵ : {"ｱｲｳｴｵ".Check_NoSymbol()}"); // 半角カタカナ
            UnityEngine.Debug.Log($"アイウエオ : {"アイウエオ".Check_NoSymbol()}"); // 全角カタカナ
            UnityEngine.Debug.Log($"漢字 : {"漢字".Check_NoSymbol()}"); // 全角漢字
            UnityEngine.Debug.Log($"1234567890 : {"1234567890".Check_NoSymbol()}"); // 半角数字
            UnityEngine.Debug.Log($"１２３４５６７８９０ : {"１２３４５６７８９０".Check_NoSymbol()}"); // 全角数字
            UnityEngine.Debug.Log($"abc : {"abc".Check_NoSymbol()}"); // 半角英字
            UnityEngine.Debug.Log($"ＡＢＣ : {"ＡＢＣ".Check_NoSymbol()}"); // 全角英字
            UnityEngine.Debug.Log($"あいうえ お : {"あいうえ お".Check_NoSymbol()}"); // " "　半角スペース
            UnityEngine.Debug.Log($"あいうえ　お : {"あいうえ　お".Check_NoSymbol()}"); // "　" 全角スペース
            UnityEngine.Debug.Log($"あいうえお\\r\\n : {"あいうえお\r\n".Check_NoSymbol()}"); // 改行(\r\n)
            UnityEngine.Debug.Log($"あいうえお\\n : {"あいうえお\n".Check_NoSymbol()}"); // 改行(\n)

            // false
            UnityEngine.Debug.Log($"あいうえお！ : {"あいうえお！".Check_NoSymbol()}"); // "!"
            UnityEngine.Debug.Log($"あいうえお、 : {"あいうえお、".Check_NoSymbol()}"); // "、"
            UnityEngine.Debug.Log($"あいうえお。 : {"あいうえお。".Check_NoSymbol()}"); // "。"
            UnityEngine.Debug.Log($"あいうえお？ : {"あいうえお？".Check_NoSymbol()}"); // "？"
            UnityEngine.Debug.Log($"あいうえお： : {"あいうえお：".Check_NoSymbol()}"); // "："
            UnityEngine.Debug.Log($"あいうえお； : {"あいうえお；".Check_NoSymbol()}"); // "；"
        }
#endif

        // 半角/全角の、ひらがな/カタカナ/漢字/数字/英字 のみOK
        // ひらがなと漢字は、半角がないので全角のみ
        // 半角/全角スペースと改行は、記号だけど特例で見逃す
        private static bool Check_NoSymbol(this string text)
            => !Regex.IsMatch(text, @"[^\u3040-\u309F\u30A0-\u30FF\uFF65-\uFF9F\u4E00-\u9FFF\u3400-\u4DBF\u0030-\u0039\uFF10-\uFF19\u0041-\u005A\u0061-\u007A\uFF21-\uFF3A\uFF41-\uFF5A\u0020\u3000\r\n]");
    }
}