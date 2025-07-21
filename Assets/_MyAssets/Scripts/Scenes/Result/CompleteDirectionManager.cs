using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Utilities;
using Scripts.ScriptableObjects;
using Image = UnityEngine.UI.Image;
using Ct = System.Threading.CancellationToken;

namespace Scripts.Scenes.Result
{
    public sealed class CompleteDirectionManager : MonoBehaviour
    {
        [SerializeField] private RectTransform creditContainer;
        [SerializeField] private AudioSource bgmAs;

        [SerializeField] private Image logo;
        [SerializeField] private Image logoOnTitle;
        [SerializeField] private Sprite logoNormalSprite;
        [SerializeField] private Sprite logoDangerSprite;
        [SerializeField] private Sprite logoNormalSpriteOnTitle;
        [SerializeField] private Sprite logoNormalSpriteOnTitleWhenCleared;

        // このスクリプト・タイトルのスクリプトののみで書き換える想定.
        public static bool DoNotUseFadeInTransitionOnTitleBegin { get; set; } = false;

        private void Start() => Play(destroyCancellationToken).Forget();

        private async UniTask Play(Ct ct)
        {
            // 初期化
            logo.SetAlpha(1);
            logoOnTitle.SetAlpha(0);
            logo.sprite = logoNormalSprite;
            creditContainer.SetAnchorPosY(0);
            creditContainer.SetScaleX(0);
            DoNotUseFadeInTransitionOnTitleBegin = true;

            // 文字をデデーン、と出す
            await 0.5f.SecAwait(ct);
            await creditContainer.DOScaleX(1, 0.3f).SetEase(Ease.InBounce).WithCancellation(ct);

            // BGMを再生する
            0.2f.SecAwaitThenDo(() => bgmAs.Raise(SSound.Entity.CompleteBGM, SoundType.BGM), ct).Forget();

            // スタッフロール
            await 2.0f.SecAwait(ct);
            await creditContainer.DOAnchorPosY(4000, 20.0f).SetEase(Ease.Linear).WithCancellation(ct);

            // ロゴを2回瞬き
            await 2.0f.SecAwait(ct);
            logo.sprite = logoDangerSprite;
            await 0.1f.SecAwait(ct);
            logo.sprite = logoNormalSprite;
            await 0.1f.SecAwait(ct);
            logo.sprite = logoDangerSprite;

            // タイトルのスプライトに徐々に切り替え
            Sprite targetSprite = SaveDataHolder.Data.HasCleared ? logoNormalSpriteOnTitleWhenCleared : logoNormalSpriteOnTitle;
            logoOnTitle.sprite = targetSprite;
            await 2.0f.SecAwait(ct);
            await DOVirtual.Float(0.0f, 1.0f, 3.0f, value =>
            {
                logo.SetAlpha(1 - value);
                logoOnTitle.SetAlpha(value);
            }).SetEase(Ease.OutQuad).WithCancellation(ct);

            await 2.0f.SecAwait(ct);

            // シーン遷移
            await bgmAs.DOFade(0, 0.3f).SetEase(Ease.InQuad).WithCancellation(ct);
            SceneId.Title.LoadAsync();
        }
    }
}