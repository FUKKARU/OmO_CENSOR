using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using Scripts.Utilities;
using Scripts.Utilities.Buttons;
using Ct = System.Threading.CancellationToken;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.Title
{
    public sealed class StartButtonManager : ATextButtonManager
    {
        [SerializeField, MinMaxSlider(0.0f, 120.0f, true)] private Vector2 bgChangeInterval;
        [SerializeField, MinMaxSlider(0.0f, 120.0f, true)] private Vector2 bgChangeIntervalWhenCleared;
        [SerializeField, MinMaxSlider(0, 1200, true)] private Vector2Int bgChangingDuration;
        [SerializeField, MinMaxSlider(0, 1200, true)] private Vector2Int bgChangingDurationWhenCleared;
        [SerializeField] private Sprite bgNormal;
        [SerializeField] private Sprite bgNormalDanger;
        [SerializeField] private Sprite bgCleared;
        [SerializeField] private Sprite bgClearedDanger;
        [SerializeField] private Image bg;
        [SerializeField] private AudioSource bgmAs;

        private void Start() => ChangeBgPeriodically(destroyCancellationToken).Forget();
        protected sealed override void OnClickSucceeded() => GoToMainScene(destroyCancellationToken).Forget();

        private async UniTask ChangeBgPeriodically(Ct ct)
        {
            Sprite sp = SaveDataHolder.Data.HasCleared ? bgCleared : bgNormal;
            Sprite spDanger = SaveDataHolder.Data.HasCleared ? bgClearedDanger : bgNormalDanger;

            BackgroundImage.enabled = false;
            Text.enabled = false;
            bg.sprite = sp;
            bg.rectTransform.SetScaleX(0);
            await 0.5f.SecAwait(ct);
            await bg.transform.DOScaleX(1, 0.3f).SetEase(Ease.InBounce).WithCancellation(ct);
            BackgroundImage.enabled = true;
            Text.enabled = true;

            0.2f.SecAwaitThenDo(() => bgmAs.Raise(SSound.Entity.TitleBGM, SoundType.BGM), ct).Forget();

            while (!ct.IsCancellationRequested)
            {
                Vector2 intervalRange = SaveDataHolder.Data.HasCleared ? bgChangeIntervalWhenCleared : bgChangeInterval;
                Vector2Int durationRange = SaveDataHolder.Data.HasCleared ? bgChangingDurationWhenCleared : bgChangingDuration;
                float interval = Random.Range(intervalRange.x, intervalRange.y);
                int duration = Random.Range(durationRange.x, durationRange.y);

                await interval.SecAwait(ct);
                bg.sprite = spDanger;
                await UniTask.DelayFrame(duration, cancellationToken: ct);
                bg.sprite = sp;
            }
        }

        private async UniTask GoToMainScene(Ct ct)
        {
            BackgroundImage.enabled = false;
            Text.enabled = false;

            await bg.transform.DOScaleX(0, 0.15f).SetEase(Ease.InBounce).WithCancellation(ct);
            await 2.0f.SecAwait(ct);

            SceneId.Main.LoadAsync();
        }
    }
}