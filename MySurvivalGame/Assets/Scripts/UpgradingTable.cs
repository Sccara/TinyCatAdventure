using UnityEngine;

public class UpgradingTable : MonoBehaviour
{
    private GameObject player;
    public GameObject upgradingUI;

    private bool isUpgradingStoneExist = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 2f && Input.GetKeyDown(KeyCode.E))
            {
                ShowUpgradingUI();
            }
            else if(Vector2.Distance(transform.position, player.transform.position) > 2.1f)
            {
                HideUpgradingUI();
            }
        }
    }

    private void ShowUpgradingUI()
    {
        upgradingUI.SetActive(!upgradingUI.activeSelf);
        upgradingUI.GetComponentInChildren<InventoryCell>().ClearSlot();
        InventoryManager.instance.selectedItem = null;
        InventoryManager.instance.selectedItemForUpgrade = null;
    }
    private void HideUpgradingUI()
    {
        upgradingUI.SetActive(false);
        upgradingUI.GetComponentInChildren<InventoryCell>().ClearSlot();
        InventoryManager.instance.selectedItem = null;
        InventoryManager.instance.selectedItemForUpgrade = null;
    }

    public void UpgradeItem()
    {
        foreach (var item in InventoryManager.instance.items)
        {
            if (item.name == "Upgrading Stone")
            {
                isUpgradingStoneExist = true;
                item.RemoveFromInventory();
                break;
            }
        }

        if (InventoryManager.instance.selectedItemForUpgrade != null && isUpgradingStoneExist)
        {
            Equipment e = (Equipment)InventoryManager.instance.selectedItemForUpgrade;

            EquipmentManager.instance.Unequip((int)e.equipSlot);

            if (e.armorModifier == 0)
            {
                e.damageModifier += 5;
            }
            else if (e.damageModifier == 0)
            {
                e.armorModifier += 1;
            }

            isUpgradingStoneExist = false;
        }
    }
}
