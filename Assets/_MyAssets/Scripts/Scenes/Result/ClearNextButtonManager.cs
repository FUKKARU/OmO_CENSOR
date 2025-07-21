using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Scripts.Utilities;
using Ct = System.Threading.CancellationToken;

namespace Scripts.Scenes.Result
{
    public sealed class ClearNextButtonManager : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake() => Impl(destroyCancellationToken).Forget();

        private async UniTask Impl(Ct ct)
        {
            button.onClick.AddListener(() => SceneId.Main.LoadAsync());

            // Do Something.
        }
    }
}