using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        // Путь, наверное, другой задать
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(savePath, jsonData);
        Debug.Log($"Game saved to: {savePath}");
    }

    public GameData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log("Game loaded!");
            return loadedData;
        }
        else
        {
            Debug.Log("No save file found. Creating new game.");
            return new GameData(); // По умолчанию
        }
    }
}