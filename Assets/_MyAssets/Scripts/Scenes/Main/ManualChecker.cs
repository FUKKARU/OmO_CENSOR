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
            // 現在の問題数に応じたManualIdを取得
            GetManualIds(ResultState.WhenClearNowLevel, out _manualIds, out _tabooManualIds);

            return 0 switch
            {
                _ when submission.Check(_tabooManualIds) == false => ResultType.Death,
                _ when submission.Check(_manualIds) == false => ResultType.Over,
                _ => ResultType.Clear,
            };
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