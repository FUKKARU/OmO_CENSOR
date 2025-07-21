using Cysharp.Threading.Tasks;
using Ct = System.Threading.CancellationToken;

namespace Scripts.Utilities
{
    public static class UniTaskExtension
    {
        public static async UniTask SecAwait(this float sec, Ct ct)
        {
            await UniTask.WaitForSeconds(sec, cancellationToken: ct);
        }

        public static async UniTask SecAwaitThenDo(this float sec, System.Action action, Ct ct)
        {
            await UniTask.WaitForSeconds(sec, cancellationToken: ct);
            action?.Invoke();
        }

        public static async UniTask SecAwaitThenAwait(this float sec, System.Func<Ct, UniTask> taskFactory, Ct ct)
        {
            await UniTask.WaitForSeconds(sec, cancellationToken: ct);
            if (taskFactory != null)
                await taskFactory(ct);
        }
    }
}
