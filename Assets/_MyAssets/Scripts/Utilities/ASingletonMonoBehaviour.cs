using UnityEngine;

namespace Scripts.Utilities
{
    public abstract class ASingletonMonoBehaviour<T> : MonoBehaviour where T : ASingletonMonoBehaviour<T>
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] instances = FindObjectsByType<T>(FindObjectsSortMode.None);
                    if (instances == null || instances.Length == 0)
                    {
                        $"No instance of {typeof(T).Name} found in the scene. Please ensure there is one instance present.".LogError();
                        return null;
                    }
                    else if (instances.Length == 1)
                    {
                        _instance = instances[0];
                    }
                    else
                    {
                        $"Multiple instances of {typeof(T).Name} found in the scene. Using the first instance and destroying others.".LogWarning();
                        _instance = instances[0];
                        for (int i = 1; i < instances.Length; ++i)
                        {
                            Destroy(instances[i]);
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
