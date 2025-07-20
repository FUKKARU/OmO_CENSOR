using System;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    public sealed class SaveData
    {
        public bool HasCleared = false;
    }

    public static class SaveDataHolder
    {
        public static SaveData Data { get; private set; } = new SaveData();
        private static readonly string Key = "SaveData";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            string json = PlayerPrefs.GetString(Key, "{}");
            Data = JsonUtility.FromJson<SaveData>(json);
        }

        public static void Save()
        {
            string json = JsonUtility.ToJson(Data);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
        }
    }
}