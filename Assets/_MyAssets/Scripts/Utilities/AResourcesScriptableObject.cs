using UnityEngine;

namespace Scripts.Utilities
{
    public abstract class AResourcesScriptableObject<T> : ScriptableObject where T : AResourcesScriptableObject<T>
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>(typeof(T).Name);

                    if (_instance == null)
                    {
                        Debug.LogError($"No instance of {typeof(T).Name} found in Resources. Please ensure it exists.");
                    }
                }

                return _instance;
            }
        }
    }
}
