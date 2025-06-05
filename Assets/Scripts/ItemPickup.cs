using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public int amount = 1;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Inventory inventory = other.transform.GetComponent<Inventory>();
            if (inventory != null && inventory.AddItem(item, amount))
            {
                Destroy(gameObject);
            }
        }
    }
}