using UnityEngine;
using Scripts.Utilities;
using Scripts.Scenes.Result;
using Unity.VisualScripting;

namespace Scripts.Scenes.Main
{
    public sealed class ManualChecker : ASingletonMonoBehaviour<ManualChecker>
    {
        private ManualId[] _manualIds = null;
        private ManualId[] _tabooManualIds = null;

        // 正誤判定を行い、どの種流のリザルトに遷移するべきか返す
        public ResultType Judge(string submission)
        {
            if (submission == null)
                return ResultType.Over;

            // 現在の問題数に応じたManualIdを取得
            GetManualIds(ResultState.WhenClearNowLevel, out _manualIds, out _tabooManualIds);

            if (submission == string.Empty)
                return _tabooManualIds.Length > 0 ? ResultType.Death : ResultType.Over;

            if (submission.Check(_tabooManualIds) == false)
                return ResultType.Death;

            if (submission.Check(_manualIds) == false)
                return ResultType.Over;

            return ResultType.Clear;
        }

        private void GetManualIds(int nowLevel, out ManualId[] manualIds, out ManualId[] tabooManualIds) => (manualIds, tabooManualIds) = nowLevel switch
        {
            1 => (new ManualId[] { ManualId.NoKanji }, new ManualId[] { }),
            2 => (new ManualId[] { ManualId.NoMuchKutoten, ManualId.NoContinuousHiragana }, new ManualId[] { }),
            3 => (new ManualId[] { ManualId.NoEvenNumber }, new ManualId[] { ManualId.NoBiggerThan99Number }),
            4 => (new ManualId[] { ManualId.NoMuchKutoten, ManualId.NoContinuousHiragana, ManualId.NoSymbol }, new ManualId[] { ManualId.NoBiggerThan99Number }),
            5 => (new ManualId[] { ManualId.NoMuchKutoten, ManualId.NoEvenNumber, ManualId.NoSymbol }, new ManualId[] { ManualId.NoBiggerThan99Number, ManualId.NoOM }),
            _ => (new ManualId[] { }, new ManualId[] { }),
        };
    }
}