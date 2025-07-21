using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using Scripts.Utilities;
using Scripts.Utilities.Buttons;
using Ct = System.Threading.CancellationToken;
using Scripts.ScriptableObjects;
using Scripts.Scenes.Result;

public class SubmitButtonManager : ATextButtonManager
{
    [SerializeField] Censorship censorship;
    [SerializeField] Scripts.Scenes.Main.ManualChecker manualChecker;
    [SerializeField] private Image bg;
    [SerializeField] GameObject hanko;
    [SerializeField] GameObject hankoMark;
    [SerializeField] private AudioSource bgmAs;

    private void Start() => ChangeBgPeriodically(destroyCancellationToken).Forget();
    protected sealed override void OnClickSucceeded() => GoToResultScene(destroyCancellationToken).Forget();

    private async UniTask ChangeBgPeriodically(Ct ct)
    {
        hankoMark.SetActive(false);
        0.2f.SecAwaitThenDo(() => bgmAs.Raise(SSound.Entity.MainBGM, SoundType.BGM), ct).Forget();
    }

    private async UniTask GoToResultScene(Ct ct)
    {
        BackgroundImage.enabled = false;
        Text.enabled = false;
        
        await bg.transform.DOScaleX(0, 0.15f).SetEase(Ease.InBounce).WithCancellation(ct);
        await 0.5f.SecAwait(ct);
        string text = censorship.RemainingText;
        if (!string.IsNullOrEmpty(text))
            text = text.Replace("■", "").Replace("\r\n", "").Replace("\n", "").Trim();
        CheckResult(manualChecker.Judge(text));
        await hanko.transform.DOMove(hankoMark.transform.position + Vector3.one * 0.5f, 1f);
        hankoMark.SetActive(true);
        await hanko.transform.DOMove(hankoMark.transform.position + Vector3.down * 2f, 0.5f);
        await 0.5f.SecAwait(ct);

        SceneId.Result.LoadAsync();
    }

    protected sealed override void PlayHoverSE() => SeAudioSource.Raise(SSound.Entity.HoverSE, SoundType.SE, 0.05f, 1, 0);

    private void CheckResult(ResultType resultType)
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
    }
}
