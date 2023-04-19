using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public RecoveryType recoveryType;

    public int recoveryAmount;

    public override void Use()
    {
        switch (recoveryType)
        {
            case RecoveryType.Health:
                PlayerStats.instance.UpdateHealth(recoveryAmount);
                break;
            case RecoveryType.Stamina:
                PlayerStats.instance.UpdateStamina(recoveryAmount);
                break;
            case RecoveryType.Cold:
                PlayerStats.instance.UpdateCold(recoveryAmount);
                break;
            default:
                Debug.Log("Recovery Type Error!");
                break;
        }

        RemoveFromInventory();
    }
}

public enum RecoveryType { Health, Stamina, Cold }
