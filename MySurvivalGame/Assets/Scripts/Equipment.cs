using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int startArmorModifier;
    public int startDamageModifier;
    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        EquipmentManager.instance.Equip(this);

        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Boots, Weapon, SecondaryWeapon, Magic}
