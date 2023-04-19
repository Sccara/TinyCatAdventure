using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    public int healthRecoveryAmount;

    public override void Use()
    {
        PlayerStats.instance.UpdateHealth(healthRecoveryAmount);

        RemoveFromInventory();
    }
}
