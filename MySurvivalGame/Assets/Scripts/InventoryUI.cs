using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform equipmentParent;

    InventoryManager inventoryManager;
    EquipmentManager equipmentManager;

    InventoryCell[] cells;
    InventoryCell[] equipmentCells;


    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = InventoryManager.instance;
        equipmentManager = EquipmentManager.instance;
        inventoryManager.onItemChangedCallback += UpdateUI;

        cells = itemsParent.GetComponentsInChildren<InventoryCell>();
        equipmentCells = equipmentParent.GetComponentsInChildren<InventoryCell>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (i < inventoryManager.items.Count)
            {
                cells[i].AddItem(inventoryManager.items[i]);
            }
            else
            {
                cells[i].ClearSlot();
            }
        }

        for (int i = 0; i < equipmentCells.Length; i++)
        {
            if (i < equipmentManager.currentEquipment.Length)
            {
                equipmentCells[i].AddItem(equipmentManager.currentEquipment[i]);
            }
            else if (equipmentManager.currentEquipment[i] == null)
            {
                equipmentCells[i].ClearSlot();
            }
        }
    }
}
