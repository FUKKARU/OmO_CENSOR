using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using Scripts.Utilities;
using Scripts.Utilities.Buttons;
using Ct = System.Threading.CancellationToken;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.Result
{
    public sealed class DeathSceneButtonManager : ATextButtonManager
    {
        [SerializeField] private Image bg;
        [SerializeField] private AudioSource bgmAs;

        protected sealed override void OnClickSucceeded() => GoToTitleScene(destroyCancellationToken).Forget();

        private void Start()
        {
            bgmAs.Raise(SSound.Entity.DeathBGM, SoundType.BGM);   
        }

        private async UniTask GoToTitleScene(Ct ct)
        {
            BackgroundImage.enabled = false;
            Text.enabled = false;

            await bg.transform.DOScaleX(0, 0.15f).SetEase(Ease.InBounce).WithCancellation(ct);
            await 1.0f.SecAwait(ct);

            SceneId.Title.LoadAsync();
        }

        protected sealed override void PlayHoverSE() => SeAudioSource.Raise(SSound.Entity.HoverSE, SoundType.SE, 0.05f, 1, 0);
    }
}