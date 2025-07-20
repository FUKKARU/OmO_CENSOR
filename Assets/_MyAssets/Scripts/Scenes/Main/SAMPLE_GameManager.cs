using UnityEngine;
using UnityEngine.UI;
using Scripts.Utilities;
using Scripts.Scenes.Result;

namespace Scripts.Scenes.Main
{
    public sealed class SAMPLE_GameManager : MonoBehaviour
    {
        [SerializeField] private Button clearButton;
        [SerializeField] private Button overButton;
        [SerializeField] private Button deathButton;

        private void Awake()
        {
            clearButton.onClick.AddListener(() => GoToResultScene(ResultType.Clear));
            overButton.onClick.AddListener(() => GoToResultScene(ResultType.Over));
            deathButton.onClick.AddListener(() => GoToResultScene(ResultType.Death));
        }

        private void GoToResultScene(ResultType resultType)
        {
            ResultState.Type = resultType;
            SceneId.Result.LoadAsync();
        }
    }
}