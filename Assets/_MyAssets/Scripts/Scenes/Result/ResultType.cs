namespace Scripts.Scenes.Result
{
    public enum ResultType : byte
    {
        Clear,
        Over,
        Death
    }

    public static class ResultState
    {
        // これを変えてからリザルトにシーン遷移して！
        public static ResultType Type { get; set; } = ResultType.Clear;
    }
}
