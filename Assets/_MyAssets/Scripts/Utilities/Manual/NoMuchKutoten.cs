namespace Scripts.Utilities
{
    public static partial class ManualChecker
    {

#if true
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Test_Check_MochKutoten()
        {
            string test1 = "����̓e�X�g�ł��B��肠��܂���B";
            string test2 = "����́A�e�X�g�ł��A��肪�A����悤�ł��B";

            UnityEngine.Debug.Log($"test1�i��Ǔ_���j: {test1.Check_NoMuchKutoten()}"); // �� true
            UnityEngine.Debug.Log($"test2�i��Ǔ_���j: {test2.Check_NoMuchKutoten()}"); // �� false

        }
#endif

        public static bool Check_NoMuchKutoten(this string text)
        {
            int count = 0;

            foreach (char c in text)
            {
                if (c == '�A' || c == '�B')
                {
                    count++;
                    if (count >= 3) return false; // ��Ǔ_��3�ȏ� �� �ᔽ
                }
            }

            return true; // ��Ǔ_��2�ȉ� �� ���e
        }


    }
}