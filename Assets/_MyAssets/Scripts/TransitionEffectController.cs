using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks; // ← 追加



namespace Animation {

    // AnimationManagerは単体用途が弱いので統合
    public class TransitionEffectController : Scripts.Utilities.ASingletonMonoBehaviour<TransitionEffectController>
    {
        [Header("トランジション用マテリアルリスト")]
        [SerializeField] private List<Material> transitionMaterials;
        [Header("遷移時間(秒)")]
        [SerializeField] private float transitionDuration = 1.5f;

        // フェードAPI（非同期対応）
        public async UniTask FadeImageAsync(Image img, float targetAlpha, float duration)
        {
            var tcs = new UniTaskCompletionSource();
            await img.DOFade(targetAlpha, duration).OnComplete(() => tcs.TrySetResult());
            await tcs.Task;
        }
        public async UniTask FadeSpriteAsync(SpriteRenderer sr, float targetAlpha, float duration)
        {
            var tcs = new UniTaskCompletionSource();
            await sr.DOFade(targetAlpha, duration).OnComplete(() => tcs.TrySetResult());
            await tcs.Task;
        }

        // カウントアニメ（非同期）
        public async UniTask AnimateCountAsync(TMP_Text text, int from, int to, float duration)
        {
            var tcs = new UniTaskCompletionSource();
            await DOTween.To(() => from, x => {
                from = x;
                text.text = from.ToString();
            }, to, duration).OnComplete(() => tcs.TrySetResult());
            await tcs.Task;
        }

        // トランジション開始（非同期対応）
        public async UniTask PlayTransitionInAsync(int materialIndex = 0)
        {
            int idx = ValidateIndex(materialIndex);
            var mat = transitionMaterials[idx];
            mat.SetFloat("_Value", 0f);
            var tcs = new UniTaskCompletionSource();
            await DOTween.To(() => mat.GetFloat("_Value"), v => mat.SetFloat("_Value", v), 1f, transitionDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => tcs.TrySetResult());
            await tcs.Task;
        }
        public async UniTask PlayTransitionOutAsync(int materialIndex = 0)
        {
            int idx = ValidateIndex(materialIndex);
            var mat = transitionMaterials[idx];
            mat.SetFloat("_Value", 1f);
            var tcs = new UniTaskCompletionSource();
            await DOTween.To(() => mat.GetFloat("_Value"), v => mat.SetFloat("_Value", v), 0f, transitionDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => tcs.TrySetResult());
            await tcs.Task;
        }
        private int ValidateIndex(int materialIndex)
        {
            if (transitionMaterials == null || transitionMaterials.Count == 0)
                throw new Exception("Transition materials not set!");
            if (materialIndex < 0 || materialIndex >= transitionMaterials.Count)
                return 0;
            return materialIndex;
        }
    }



}




