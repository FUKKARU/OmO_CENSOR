using UniSceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Scripts.Utilities
{
    public enum SceneId : byte
    {
        Title,
        Main,
        Result, // 一旦Resultに遷移し、その後対応するシーンに遷移
        Result_Clear,
        Result_Complete,
        Result_Over,
        Result_Death,
    }

    public static class SceneManager
    {
        public static void LoadAsync(this SceneId sceneId)
            => UniSceneManager.LoadSceneAsync(sceneId.ToStr());

        private static string ToStr(this SceneId sceneId) => sceneId switch
        {
            SceneId.Title => "Title",
            SceneId.Main => "Main",
            SceneId.Result => "Result",
            SceneId.Result_Clear => "Clear",
            SceneId.Result_Complete => "Complete",
            SceneId.Result_Over => "Over",
            SceneId.Result_Death => "Death",
            _ => string.Empty,
        };
    }
}