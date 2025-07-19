#pragma warning disable 1998 // 1998: Asyncメソッドでawaitがない場合の警告を無視

using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Sound
{
    public enum SoundType
    {
        SE,   // 効果音
        BGM   // BGM
    }

    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip clip;
        public SoundType type;
    }

    public class SoundManager : Scripts.Utilities.ASingletonMonoBehaviour<SoundManager>
    {
        [Header("サウンドデータリスト")]
        [SerializeField] private List<SoundData> soundList;
        [Header("BGM用AudioSource")]
        [SerializeField] private AudioSource bgmSource;
        [Header("SE用AudioSource(複数可)")]
        [SerializeField] private List<AudioSource> seSources;
        [SerializeField] private int seSourcePoolSize = 5;

        private Dictionary<string, SoundData> soundDict = new Dictionary<string, SoundData>();

        void Awake()
        {

            //DontDestroyOnLoad(gameObject);
            foreach (var s in soundList)
            {
                if (!soundDict.ContainsKey(s.name))
                    soundDict.Add(s.name, s);
            }
            // SE用AudioSourceプール構築
            if (seSources == null || seSources.Count < seSourcePoolSize)
            {
                seSources = new List<AudioSource>();
                for (int i = 0; i < seSourcePoolSize; i++)
                {
                    var src = gameObject.AddComponent<AudioSource>();
                    src.playOnAwake = false;
                    seSources.Add(src);
                }
            }
        }

        public async UniTask PlayBGMAsync(string name, bool loop = true)
        {
            if (!soundDict.TryGetValue(name, out var data) || data.type != SoundType.BGM) return;
            if (bgmSource == null)
            {
                bgmSource = gameObject.AddComponent<AudioSource>();
                bgmSource.loop = true;
            }
            bgmSource.clip = data.clip;
            bgmSource.loop = loop;
            bgmSource.Play();
            // BGM再生完了までawaitしたい場合↓
            // await UniTask.WaitUntil(() => !bgmSource.isPlaying);
        }

        public void StopBGM()
        {
            if (bgmSource != null)
                bgmSource.Stop();
        }

        public async UniTask PlaySEAsync(string name, float volume = 1f)
        {
            if (!soundDict.TryGetValue(name, out var data) || data.type != SoundType.SE) return;
            foreach (var src in seSources)
            {
                if (!src.isPlaying)
                {
                    src.clip = data.clip;
                    src.volume = volume;
                    src.Play();
                    await UniTask.WaitUntil(() => !src.isPlaying);
                    return;
                }
            }
            // プールが埋まっていたら追加生成も可
        }

        public void StopAllSE()
        {
            foreach (var src in seSources)
            {
                src.Stop();
            }
        }
    }
}