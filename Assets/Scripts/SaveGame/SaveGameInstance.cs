using System;
using System.IO;
using UnityEngine;

namespace Project
{
    using Data;

    public static class SaveGameInstance
    {
        public static bool Load(string saveName, out SaveGameData? data)
        {
            string savePath = GetSavePath(saveName);
            if (!File.Exists(savePath))
            {
                Debug.LogError($"Save file {saveName} not found!");
                data = null;
                return false;
            }
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<SaveGameData>(json);
            return true;
        }

        public static bool Save(string saveName, SaveGameData saveGameData)
        {
            string savePath = GetSavePath(saveName);
            try
            {
                string json = JsonUtility.ToJson(saveGameData, true);
                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("Failed to serialize save data!");
                    return false;
                }
                File.WriteAllText(savePath, json);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save data: {e.Message}");
                return false;
            }
        }

        private static string GetSavePath(string saveName)
        {
            return $"{Application.persistentDataPath}/{saveName}.json";
        }
    }
}