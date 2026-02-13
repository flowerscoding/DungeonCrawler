using System.Collections.Generic;
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
        public LoadSystem.Scene scene;
        public List<ItemData> storage = new List<ItemData>();
        public List<ItemData> storageConsumableItems = new List<ItemData>();
        public List<ItemData> storageArmorItems = new List<ItemData>();
        public List<ItemData> storageKeyItems = new List<ItemData>();
    }
    public void SaveGame()
    {
        currentData = new SaveData
        {
            health = Player.instance.playerData.curHealth,
            scene = Player.instance.playerData.currentScene,
            position = Player.instance.playerData.worldPos,
            storage = Inventory.Instance.inventoryStorage.storage,
            storageConsumableItems = Inventory.Instance.inventoryStorage.storageConsumableItems,
            storageArmorItems = Inventory.Instance.inventoryStorage.storageArmorItems,
            storageKeyItems = Inventory.Instance.inventoryStorage.storageKeyItems,
        };

        string json = JsonUtility.ToJson(currentData, true);
        File.WriteAllText(savePath, json);
    }
    SaveData LoadGame()
    {
        if (!File.Exists(savePath)) return null;
        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
    public void LoadSaveData()
    {
        currentData = LoadGame();
        Player.instance.LoadPlayerData();
        Inventory.Instance.inventoryStorage.LoadInventory();
    }
}
