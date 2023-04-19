using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton

    public static InventoryManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one InventoryManager instances");
            return;
        }
        instance = this;
    }

    #endregion

    public List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public Item selectedItem;
    public Item selectedItemForUpgrade;

    public int space = 20;

    private void Update()
    {
        if (PlayerStats.instance.quest.isActive)
        {
            CheckQuest();
        }
    }

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough space");
            return false;
        }

        if (item.isEquipmentItem)
        {
            Equipment e = (Equipment)item;

            if (e.isPickedUp == false)
            {
                e.damageModifier = e.startDamageModifier;
                e.armorModifier = e.startArmorModifier;
            }

            items.Add(e);
        }
        else
        {
            items.Add(item);
        }


        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void CheckQuest()
    {
        float amount = 0;

        foreach (Item item in items)
        {
            if (item.name == PlayerStats.instance.quest.requiredItem.name)
            {
                amount++;
            }
        }

        if (amount == PlayerStats.instance.quest.itemRequiredAmount)
        {
            PlayerStats.instance.questIsCompleted = true;
        }
    }
}
