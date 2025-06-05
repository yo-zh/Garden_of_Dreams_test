using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Button deleteButton;

    private Inventory inventory;
    private int slotIndex;

    public void Setup(Inventory inventory, int slotIndex)
    {
        this.inventory = inventory;
        this.slotIndex = slotIndex;
        deleteButton.gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(ToggleDelete);
        deleteButton.onClick.AddListener(OnDeleteClicked);
    }

    public void UpdateSlot(Item item, int amount)
    {
        if (item != null)
        {
            deleteButton.gameObject.SetActive(true);
        }
        else
        {
            deleteButton.gameObject.SetActive(false);
        }
    }

    private void ToggleDelete()
    {
        deleteButton.gameObject.SetActive(!deleteButton.gameObject.activeSelf);
    }

    private void OnDeleteClicked()
    {
        Debug.Log("Delete pressed!");
        inventory.RemoveItem(slotIndex);
        ToggleDelete();
    }
}