using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // JSON
public class GameData
{
    public Vector3 playerPosition;
    public float playerHealth;
    public List<InventoryItem> inventoryItems;

    [Serializable]
    public class InventoryItem
    {
        public string itemId;
        public int amount;
    }
}