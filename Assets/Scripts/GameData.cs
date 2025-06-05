using System.Collections.Generic;

[System.Serializable] // JSON
public class GameData
{
    public List<InventorySlotData> inventoryItems;
    public int maxSlots;

    public GameData()
    {
        inventoryItems = new List<InventorySlotData>();
        maxSlots = 5;
    }
}

[System.Serializable]
public class InventorySlotData
{
    public string itemName;
    public int amount;

    public InventorySlotData(string name, int count)
    {
        itemName = name;
        amount = count;
    }
}