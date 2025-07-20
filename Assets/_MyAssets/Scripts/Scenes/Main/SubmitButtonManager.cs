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
            GoToResultScene(manualChecker.Judge(censorship.RemainingText.Replace("□", "")));
        });
    }

    private void GoToResultScene(ResultType resultType)
    {
        ResultState.Type = resultType;
        Debug.Log(resultType);
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
