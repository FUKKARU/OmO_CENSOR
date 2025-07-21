using Scripts.Scenes.Result;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

namespace Scripts.Scenes.Main
{
    public sealed class ManualShower : MonoBehaviour
    {
        [SerializeField, Header("順に設定")] private Text[] texts;

        private string[] sentences = null;

        private void Start()
        {
            sentences = SManualData.Entity.Get(ResultState.WhenClearNowLevel);
            UpdateTexts(texts, sentences);
        }

        private void UpdateTexts(Text[] texts, string[] sentences)
        {
            if (texts == null) return;

            if (sentences == null)
            {
                foreach (var text in texts)
                {
                    text.text = string.Empty;
                }
                return;
            }

            if (texts.Length < sentences.Length)
            {
                Debug.LogWarning($"ManualShower: textsの数({texts.Length})がsentencesの数({sentences.Length})より少ないです。");
                return;
            }

            for (int i = 0; i < texts.Length; i++)
            {
                if (i < sentences.Length)
                {
                    texts[i].text = sentences[i];
                }
                else
                {
                    texts[i].text = string.Empty; // 余分なテキストは空にする
                }
            }
        }
    }
}