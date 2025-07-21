using UnityEngine;

namespace Scripts.Utilities
{
    public abstract class AResourcesScriptableObject<T> : ScriptableObject where T : AResourcesScriptableObject<T>
    {
        private static T _entity = null;

        public static T Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<T>(typeof(T).Name);

                    if (_entity == null)
                    {
                        $"No instance of {typeof(T).Name} found in Resources. Please ensure it exists.".LogError();
                    }
                }

                return _entity;
            }
        }
    }
}
