using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isCompleted;
    public bool isActive;

    public string title;
    public string description;
    public int moneyReward;
    public int itemRequiredAmount;
    public int itemRewardAmount;
    public Item requiredItem;
    public Item itemReward;
}
