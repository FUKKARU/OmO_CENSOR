using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Utilities;
using Scripts.Utilities.Buttons;
using Scripts.ScriptableObjects;
using Ct = System.Threading.CancellationToken;

namespace Scripts.Scenes.Result
{
    public sealed class DeathSceneButtonManager : ATextButtonManager
    {
        [SerializeField] private GameObject bg;
        [SerializeField] private AudioSource bgmAs;

        private void Start() => AnimBg(destroyCancellationToken).Forget();
        protected sealed override void OnClickSucceeded() => GoToTitleScene(destroyCancellationToken).Forget();

        private async UniTask AnimBg(Ct ct)
        {
            BackgroundImage.enabled = false;
            Text.enabled = false;
            bg.transform.SetScaleX(0);
            await 0.5f.SecAwait(ct);
            await bg.transform.DOScaleX(1, 0.3f).SetEase(Ease.InBounce).WithCancellation(ct);
            BackgroundImage.enabled = true;
            Text.enabled = true;

            0.2f.SecAwaitThenDo(() => bgmAs.Raise(SSound.Entity.TitleBGM, SoundType.BGM, pitch: 0.5f), ct).Forget();
        }

        private async UniTask GoToTitleScene(Ct ct)
        {
            BackgroundImage.enabled = false;
            Text.enabled = false;

            await bg.transform.DOScaleX(0, 0.15f).SetEase(Ease.InBounce).WithCancellation(ct);
            bgmAs.DOFade(0, 0.3f).SetEase(Ease.InQuad).WithCancellation(ct).Forget();
            await 0.5f.SecAwait(ct);

            SceneId.Title.LoadAsync();
        }

        protected sealed override void PlayHoverSE() => SeAudioSource.Raise(SSound.Entity.HoverSE, SoundType.SE, 0.05f, 1, 0);
    }
}