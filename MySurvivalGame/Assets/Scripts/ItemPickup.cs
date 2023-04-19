using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        item.isPickedUp = false;
    }

    void PickUp()
    {
        if (item.isMoney)
        {
            PlayerStats.instance.money += item.value;
            item.isPickedUp = true;
            Destroy(gameObject);
        }
        else
        {
            bool wasPickedUp = InventoryManager.instance.Add(item);

            if (wasPickedUp)
            {
                item.isPickedUp = true;
                Destroy(gameObject);
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PickUp();
        }
    }
}
