using UnityEngine;
using Scripts.Utilities;

namespace Scripts.Scenes.Result
{
    public sealed class ResultSceneManager : MonoBehaviour
    {
        private void Awake()
        {
            switch (ResultState.Type)
            {
                case ResultType.Clear:
                    if (ResultState.WhenClearNowLevel > ResultState.MaxLevel)
                    {
                        ResultState.WhenClearNowLevel = 1;
                        SceneId.Result_Complete.LoadAsync();
                    }
                    else
                    {
                        SceneId.Result_Clear.LoadAsync();
                    }
                    break;
                case ResultType.Over:
                    {
                        SceneId.Result_Over.LoadAsync();
                    }
                    break;
                case ResultType.Death:
                    {
                        ResultState.WhenClearNowLevel = 1;
                        SceneId.Result_Death.LoadAsync();
                    }
                    break;
            }
        }
    }
}