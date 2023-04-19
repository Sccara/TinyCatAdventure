using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one EquipmentManager instances");
            return;
        }

        instance = this;
    }

    #endregion

    public Equipment[] currentEquipment;
    public GameObject[] currentEquipmentObjects;


    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventoryManager.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        currentEquipmentObjects[slotIndex].GetComponent<InventoryCell>().AddItem(newItem);
    }    

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            
            inventoryManager.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            currentEquipmentObjects[slotIndex].GetComponent<InventoryCell>().ClearSlot();
        }
    }

    private void Update()
    {
        
    }
}
