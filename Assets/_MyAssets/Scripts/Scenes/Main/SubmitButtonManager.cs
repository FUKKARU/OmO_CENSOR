using UnityEngine;
using UnityEngine.UI;
using Scripts.Utilities;
using Scripts.Scenes.Result;

public class SubmitButtonManager : MonoBehaviour
{
    [SerializeField] Censorship censorship;
    [SerializeField] Scripts.Scenes.Main.ManualChecker manualChecker;
    [SerializeField] Button btn;

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            string text = censorship.RemainingText;
            if (!string.IsNullOrEmpty(text))
                text = text.Replace("□", "").Replace("\r\n", "").Replace("\n", "").Trim();
            GoToResultScene(manualChecker.Judge(text));
        });
    }

    private void GoToResultScene(ResultType resultType)
    {
        ResultState.Type = resultType;
        {
            if (resultType == ResultType.Clear)
            {
                if (ResultState.WhenClearNowLevel >= 5)
                {
                    ResultState.WhenClearNowLevel = 1; // 出世したら、タイトルへ
                }
                else
                {
                    ResultState.WhenClearNowLevel++; // クリアしたら次のステージへ                    
                }
            }
            else if (resultType == ResultType.Over) { } // オーバーしたらそのステージをもう一度
            else
                ResultState.WhenClearNowLevel = 1; // 磔になったら終わり、タイトルへ
        }

        SceneId.Result.LoadAsync();
    }
}
