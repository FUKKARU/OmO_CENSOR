using Scripts.Utilities;
using Scripts.Scenes.Result;

namespace Scripts.Scenes.Main
{
    public sealed class ManualChecker : ASingletonMonoBehaviour<ManualChecker>
    {
        private ManualId[] _manualIds = null;
        private ManualId[] _tabooManualIds = null;

        // 正誤判定を行い、どの種流のリザルトに遷移するべきか返す
        public ResultType Judge(string submission)
        {
            if (string.IsNullOrEmpty(submission))
                return ResultType.Over;

            // 現在の問題数に応じたManualIdを取得
            GetManualIds(ResultState.WhenClearNowLevel, out _manualIds, out _tabooManualIds);

            if (_tabooManualIds != null && submission.Check(_tabooManualIds) == false)
                return ResultType.Death;

            if (_manualIds != null && submission.Check(_manualIds) == false)
                return ResultType.Over;

            return ResultType.Clear;
        }

        private void GetManualIds(int nowLevel, out ManualId[] manualIds, out ManualId[] tabooManualIds) => (manualIds, tabooManualIds) = nowLevel switch
        {
            1 => (new ManualId[] { ManualId.NoKanji }, null),
            2 => (new ManualId[] { ManualId.NoMuchKutoten, ManualId.NoContinuousHiragana }, null),
            3 => (new ManualId[] { ManualId.NoEvenNumber, ManualId.NoOddStrokesHiragana }, new ManualId[] { ManualId.NoBiggerThan99Number }),
            4 => (new ManualId[] { ManualId.NoMuchKutoten, ManualId.NoContinuousHiragana, ManualId.NoSymbol }, new ManualId[] { ManualId.NoBiggerThan99Number }),
            5 => (new ManualId[] { ManualId.NoMuchKutoten, ManualId.NoEvenNumber, ManualId.NoOddStrokesHiragana, ManualId.NoSymbol }, new ManualId[] { ManualId.NoBiggerThan99Number, ManualId.NoOM }),
            _ => (null, null),
        };
    }
}