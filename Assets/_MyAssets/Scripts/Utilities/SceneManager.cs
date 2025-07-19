using UniSceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Scripts.Utilities
{
    public enum SceneId : byte
    {
        Title,
        Main,
        Result, // クリア、オーバー、磔の刑、出世エンディング
    }

    public static class SceneManager
    {
        public static void LoadAsync(this SceneId sceneId)
        {
            string sceneName = sceneId.ToString();
            UniSceneManager.LoadSceneAsync(sceneName);
        }
    }
}