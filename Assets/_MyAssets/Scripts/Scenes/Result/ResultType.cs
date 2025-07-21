namespace Scripts.Scenes.Result
{
    public enum ResultType : byte
    {
        Clear,
        Over,
        Death
    }

    // ここの変数を更新してから、リザルトにシーン遷移して！
    public static class ResultState
    {
        public static ResultType Type { get; set; } = ResultType.Clear;

        public static readonly int MaxLevel = 5; // [1,5]
        public static int WhenClearNowLevel = 1;  // [1,5]
    }
}
