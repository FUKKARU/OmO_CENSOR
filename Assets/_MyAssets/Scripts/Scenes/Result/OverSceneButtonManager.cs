using UnityEngine;
using UnityEngine.UI;
using Scripts.Utilities;

namespace Scripts.Scenes.Result
{
    public sealed class OverSceneButtonManager : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(() => SceneId.Main.LoadAsync());
        }
    }
}