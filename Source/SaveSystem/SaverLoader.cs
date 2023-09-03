using UnityEngine;

namespace SaveSystem
{
    public static class SaverLoader
    {
        public static T LoadData<T>(string key) where T : new()
        {
            if (PlayerPrefs.HasKey(key))
            {
                string loadedStringData = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(loadedStringData);
            }
            else
            {
                return new T();
            }
        }

        public static void SaveData<T>(string key, T dataType)
        {
            string jsonDataString = JsonUtility.ToJson(dataType, true);
            PlayerPrefs.SetString(key, jsonDataString);
        }
    }
}