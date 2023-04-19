using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;

    private bool mouse_over;

    public Item item;

    public Vector3 offset;

    private void Update()
    {
        if (mouse_over)
        {
            if (item != null)
            {
                ShowInfoMenu();
            }
        }
    }

    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
        }
    }

    public void SelectItem()
    {
        if (item != null)
        {
            InventoryManager.instance.selectedItem = item;
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            SelectItem();

            if (item.isEquipmentItem || item.isInteractableItem)
            {
                item.Use();
            }
        }

        GameManager.instance.infoMenu.SetActive(false);
    }    

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void PutSelectedItem()
    {
        if (item != null)
        {
            ClearSlot();
        }
        else
        {
            if (InventoryManager.instance.selectedItem != null && InventoryManager.instance.selectedItem.isEquipmentItem)
            {
                item = InventoryManager.instance.selectedItem;
                InventoryManager.instance.selectedItemForUpgrade = item;

                icon.sprite = item.icon;
                icon.enabled = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        GameManager.instance.infoMenu.SetActive(false);
    }

    private void ShowInfoMenu()
    {
        GameManager.instance.infoMenu.SetActive(true);

        GameManager.instance.nameText.text = item.name;

        if (!item.isEquipmentItem)
        {
            GameManager.instance.statsText.text = "";
        }
        else
        {
            Equipment e = (Equipment)item;

            GameManager.instance.statsText.text = "ATTACK: " + e.damageModifier + "\tARMOUR: " + e.armorModifier;
        }

        GameManager.instance.descriptionText.text = "Some description";

        GameManager.instance.infoMenu.transform.position = transform.position + offset;
    }
}
