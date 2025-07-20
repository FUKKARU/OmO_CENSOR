using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Scripts.Utilities;
using Scripts.Utilities.Buttons;
using Ct = System.Threading.CancellationToken;

namespace Scripts.Scenes.Title
{
    public sealed class StartButtonManager : ATextButtonManager
    {
        [SerializeField] private Image bg;

        protected sealed override void OnClickSucceeded() => GoToMainScene(destroyCancellationToken).Forget();

        private async UniTask GoToMainScene(Ct ct)
        {
            SceneId.Main.LoadAsync();
        }
    }
}