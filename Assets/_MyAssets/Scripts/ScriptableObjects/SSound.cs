using UnityEngine;
using Scripts.Utilities;
using UnityEngine.Audio;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SSound", menuName = "ScriptableObjects/SSound")]
    public sealed class SSound : AResourcesScriptableObject<SSound>
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMixerGroup _AMGroupBGM;
        [SerializeField] private AudioMixerGroup _AMGroupSE;
        [SerializeField] private AudioMixerGroup _AMGroupMaster;

        public AudioMixer AudioMixer => _audioMixer;
        public AudioMixerGroup AMGroupBGM => _AMGroupBGM;
        public AudioMixerGroup AMGroupSE => _AMGroupSE;
        public AudioMixerGroup AMGroupMaster => _AMGroupMaster;

        [Space(25)]
        [Header("BGM")]
        [SerializeField] private AudioClip _titleBGM;
        [SerializeField] private AudioClip _mainBGM;
        [SerializeField] private AudioClip _clearBGM;
        [SerializeField] private AudioClip _completeBGM;
        [SerializeField] private AudioClip _overBGM;
        [SerializeField] private AudioClip _deathBGM;

        public AudioClip TitleBGM => _titleBGM;
        public AudioClip MainBGM => _mainBGM;
        public AudioClip ClearBGM => _clearBGM;
        public AudioClip CompleteBGM => _completeBGM;
        public AudioClip OverBGM => _overBGM;
        public AudioClip DeathBGM => _deathBGM;

        [Space(25)]
        [Header("SE")]
        [SerializeField] private AudioClip _clickSE;
        [SerializeField] private AudioClip _hoverSE;

        public AudioClip ClickSE => _clickSE;
        public AudioClip HoverSE => _hoverSE;
    }
}