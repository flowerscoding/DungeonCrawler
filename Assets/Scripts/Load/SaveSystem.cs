using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    string savePath;
    public SaveData currentData;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            savePath = Application.persistentDataPath + "/save.json";
        }
        else Destroy(gameObject);
    }
    [System.Serializable]
    public class SaveData
    {
        public int health;
        public Vector3 position;
        public LoadSystem.SceneType scene;
        //public WeaponData equippedWeapon;
    }
    public void SaveGame()
    {
        currentData = new SaveData
        {
            health = Player.instance.playerData.curHealth,
            scene = Player.instance.playerData.currentScene,
            position = Player.instance.playerData.worldPos,
        };

        string json = JsonUtility.ToJson(currentData, true);
        File.WriteAllText(savePath, json);
    }
    SaveData LoadGame()
    {
        if (!File.Exists(savePath))return null;
        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
    public void LoadSaveData()
    {
        currentData = LoadGame();

        Player.instance.UpdatePlayerData();
    }
}
