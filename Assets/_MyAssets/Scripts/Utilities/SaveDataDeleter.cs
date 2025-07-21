#if false
using UnityEngine;

namespace Scripts.Utilities
{
    public static class SaveDataDeleter
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Impl()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
#endif
