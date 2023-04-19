using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    #region Singleton

    public static QuestManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one QuestManager instances");
            return;
        }
        instance = this;
    }

    #endregion

    public GameObject questWindowUI;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI moneyRewardText;
    public TextMeshProUGUI itemRequireText;
    public TextMeshProUGUI itemRewardText;

    public Image itemRequireImage;
    public Image itemRewardImage;

    private PlayerStats player;

    private void Start()
    {
        player = PlayerStats.instance;
    }


    public void OnClickQuestButton()
    {
        if (player.quest.isActive)
        {
            UpdateUI();
        }
        else
        {
            questWindowUI.SetActive(!questWindowUI.activeSelf);
            titleText.text = "";
            descriptionText.text = "No active quest now! Talk to someone to get quest!";
            moneyRewardText.text = "";
            itemRequireText.text = "";
            itemRewardText.text = "";
            itemRequireImage.enabled = false;
            itemRewardImage.enabled = false;
        }
    }

    public void UpdateUI()
    {
        questWindowUI.SetActive(!questWindowUI.activeSelf);
        titleText.text = player.quest.title;
        descriptionText.text = player.quest.description;
        moneyRewardText.text = "Meow Coin x" + player.quest.moneyReward.ToString();
        itemRequireText.text = player.quest.requiredItem.name + " x" + player.quest.itemRequiredAmount.ToString();
        itemRewardText.text = player.quest.itemReward.name + " x" + player.quest.itemRewardAmount.ToString();

        itemRequireImage.enabled = true;
        itemRewardImage.enabled = true;
        itemRequireImage.sprite = player.quest.requiredItem.icon;
        itemRewardImage.sprite = player.quest.itemReward.icon;
    }
}
