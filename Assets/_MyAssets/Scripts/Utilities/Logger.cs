using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Scripts
{
    public static class Logger
    {
        private const string SIMBLE = "DEBUG_LOGGER";

        [Conditional(SIMBLE)]
        public static void Log(this object obj) => Debug.Log(obj);

        [Conditional(SIMBLE)]
        public static void LogWarning(this object obj) => Debug.LogWarning(obj);

        [Conditional(SIMBLE)]
        public static void LogError(this object obj) => Debug.LogError(obj);
    }
}
