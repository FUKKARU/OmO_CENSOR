using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Utilities;
using Scripts.Scenes.Result;

namespace Scripts.Scenes.Main
{
    public sealed class SAMPLE_GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Button clearButton;
        [SerializeField] private Button overButton;
        [SerializeField] private Button deathButton;

        private void Awake()
        {
            label.text = $"メイン（{ResultState.WhenClearNowLevel}問目）です";
            clearButton.onClick.AddListener(() => GoToResultScene(ResultType.Clear));
            overButton.onClick.AddListener(() => GoToResultScene(ResultType.Over));
            deathButton.onClick.AddListener(() => GoToResultScene(ResultType.Death));
        }

        private void GoToResultScene(ResultType resultType)
        {
            ResultState.Type = resultType;

            {
                if (resultType == ResultType.Clear)
                    ResultState.WhenClearNowLevel++; // クリアしたら次のステージへ
                else if (resultType == ResultType.Over) { } // オーバーしたらそのステージをもう一度
                else
                    ResultState.WhenClearNowLevel = 1; // 磔になったら終わり、タイトルへ
            }

            SceneId.Result.LoadAsync();
        }
    }
}