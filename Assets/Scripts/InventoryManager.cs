using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private Item[] allItems;

    private void Start()
    {
        LoadInventory();
    }

    public void SaveInventory()
    {
        inventory.SaveInventory(saveSystem);
    }

    public void LoadInventory()
    {
        inventory.LoadInventory(saveSystem, allItems);
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }
}