using UnityEngine;
using UnityEngine.UI;
using Scripts.Utilities;

namespace Scripts.Scenes.Result
{
    public sealed class CompleteSceneButtonManager : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            SaveDataHolder.Data.HasCleared |= true;
            SaveDataHolder.Save();

            button.onClick.AddListener(() => SceneId.Title.LoadAsync());
        }
    }
}