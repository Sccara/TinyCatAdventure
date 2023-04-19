using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isEquipmentItem = false;
    public bool isMoney = false;
    public bool isInteractableItem = false;

    public bool isPickedUp = false;
    public float value = 0;

    public virtual void Use()
    {

    }

    public void RemoveFromInventory()
    {
        InventoryManager.instance.Remove(this);
    }
}
