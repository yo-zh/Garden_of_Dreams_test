using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Item;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private Image[] inventorySlots;
    [SerializeField] private InventorySlotUI[] slotUIs;

    public List<InventorySlot> items = new List<InventorySlot>();
    public int maxSlots = 5;

    private void Start()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            slotUIs[i].Setup(this, i);
        }
        UpdateUI();
    }


    private void OnInventory(InputValue value)
    {
        if (inventory != null)
        {
            inventory.SetActive(!inventory.activeSelf);
        }

    }

    public bool AddItem(Item itemToAdd, int amount = 1)
    {
        if (itemToAdd.isStackable)
        {
            foreach (InventorySlot slot in items)
            {
                if (slot.item == itemToAdd && slot.CanAddToStack(amount))
                {
                    slot.amount += amount;
                    UpdateUI();
                    return true;
                }
            }
        }

        if (items.Count < maxSlots)
        {
            items.Add(new InventorySlot(itemToAdd, amount));
            UpdateUI();
            return true;
        }

        Debug.Log("Inventory is full!");
        return false;
    }
    public bool CheckItem(Item interestItem)
    {
        foreach (InventorySlot slot in items)
        {
            if (slot.item == interestItem)
            {
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Item itemToRemove, int amount = 1)
    {
        List<InventorySlot> matchingSlots = items.FindAll(slot => slot.item == itemToRemove);

        if (matchingSlots.Count == 0) return false;

        int totalAmount = matchingSlots.Sum(slot => slot.amount);

        if (totalAmount < amount) return false;

        for (int i = matchingSlots.Count - 1; i >= 0 && amount > 0; i--)
        {
            InventorySlot slot = matchingSlots[i];
            int removeAmount = Mathf.Min(amount, slot.amount);
            slot.amount -= removeAmount;
            amount -= removeAmount;

            if (slot.amount <= 0)
            {
                items.Remove(slot);
            }
        }

        UpdateUI();
        return true;
    }
    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < items.Count)
        {
            items.RemoveAt(slotIndex);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < items.Count)
            {
                inventorySlots[i].sprite = items[i].item.icon;
                inventorySlots[i].enabled = true;

                if (items[i].item.isStackable && items[i].amount > 1)
                {
                    inventorySlots[i].GetComponentInChildren<TextMeshProUGUI>().text = items[i].amount.ToString();
                }
                else
                {
                    inventorySlots[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
            else
            {
                inventorySlots[i].enabled = false;
                inventorySlots[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
}
