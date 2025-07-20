using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Utilities;

namespace Scripts.Scenes.Result
{
    public sealed class SAMPLE_GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Button whenClearNextButton;
        [SerializeField] private Button whenClearClearButton;
        [SerializeField] private Button whenOverRetryButton;
        [SerializeField] private Button whenDeathBackButton;

        private void Awake()
        {
            switch (ResultState.Type)
            {
                case ResultType.Clear:
                    {
                        if (ResultState.WhenClearNowLevel > ResultState.MaxLevel)
                        {
                            // クリア
                            label.text = "リザルト（出世クリア）です";
                            whenClearNextButton.gameObject.SetActive(false);
                            whenClearClearButton.gameObject.SetActive(true);
                            whenOverRetryButton.gameObject.SetActive(false);
                            whenDeathBackButton.gameObject.SetActive(false);
                            whenClearClearButton.onClick.AddListener(() => SceneId.Title.LoadAsync());

                            SaveDataHolder.Data.HasCleared |= true;
                            SaveDataHolder.Save();
                        }
                        else
                        {
                            // 次の問題へ
                            label.text = $"リザルト（次{ResultState.WhenClearNowLevel}問目）です";
                            whenClearNextButton.gameObject.SetActive(true);
                            whenClearClearButton.gameObject.SetActive(false);
                            whenOverRetryButton.gameObject.SetActive(false);
                            whenDeathBackButton.gameObject.SetActive(false);
                            whenClearNextButton.onClick.AddListener(() => SceneId.Main.LoadAsync());
                        }
                    }
                    break;
                case ResultType.Over:
                    {
                        // タイトルへ
                        label.text = "リザルト（オーバー）です";
                        whenClearNextButton.gameObject.SetActive(false);
                        whenClearClearButton.gameObject.SetActive(false);
                        whenOverRetryButton.gameObject.SetActive(true);
                        whenDeathBackButton.gameObject.SetActive(false);
                        whenOverRetryButton.onClick.AddListener(() => SceneId.Main.LoadAsync());
                    }
                    break;
                case ResultType.Death:
                    {
                        // タイトルへ
                        label.text = "リザルト（磔）です";
                        whenClearNextButton.gameObject.SetActive(false);
                        whenClearClearButton.gameObject.SetActive(false);
                        whenOverRetryButton.gameObject.SetActive(false);
                        whenDeathBackButton.gameObject.SetActive(true);
                        whenDeathBackButton.onClick.AddListener(() => SceneId.Title.LoadAsync());
                    }
                    break;
            }
        }
    }
}