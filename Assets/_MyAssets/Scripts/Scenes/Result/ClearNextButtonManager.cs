using Scripts.Utilities;
using Scripts.Utilities.Buttons;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.Result
{
    public sealed class ClearNextButtonManager : ATextButtonManager
    {
        protected sealed override void OnClickSucceeded()
        {
            SceneId.Main.LoadAsync();
        }

        protected sealed override void PlayHoverSE() => SeAudioSource.Raise(SSound.Entity.HoverSE, SoundType.SE, 0.05f, 1, 0);
    }
}