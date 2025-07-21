using Scripts.Utilities;
using Scripts.Utilities.Buttons;

namespace Scripts.Scenes.Result
{
    public sealed class ClearNextButtonManager : ATextButtonManager
    {
        protected sealed override void OnClickSucceeded()
        {
            SceneId.Main.LoadAsync();
        }
    }
}