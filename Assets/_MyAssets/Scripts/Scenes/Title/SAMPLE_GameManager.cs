using UnityEngine;
using UnityEngine.UI;
using Scripts.Utilities;

namespace Scripts.Scenes.Title
{
    public sealed class SAMPLE_GameManager : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        private void Awake()
        {
            startButton.onClick.AddListener(() => SceneId.Main.LoadAsync());
        }
    }
}