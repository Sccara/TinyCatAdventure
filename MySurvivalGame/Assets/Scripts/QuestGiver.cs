using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public GameObject player;

    private GameObject closest;
    private static float closestDist = 2;
    public float dist;
    public bool isCompleted = false;
    public bool isDone = false;

    public void Update()
    {
        closest = GameManager.instance.FindClosestInteractableObject(2);

        if (closest != null)
        {
            closestDist = (closest.transform.position - player.transform.position).magnitude;
        }

        dist = (transform.position - player.transform.position).magnitude;

        var dialogueInteractable = GetComponent<DialogueInteractable>();

        if (Input.GetKeyDown(KeyCode.E) && dist <= closestDist && dialogueInteractable.isInteracted && !PlayerStats.instance.isBusy && !isDone)
        {
            if (quest.isActive)
            {
                CompleteQuest();
            }

            if (!isDone)
            {
                GiveQuest();
            }
        }
    }

    public void GiveQuest()
    {
        if (!PlayerStats.instance.quest.isActive)
        {
            quest.isActive = true;
            PlayerStats.instance.quest = quest;
            QuestManager.instance.UpdateUI();
            QuestManager.instance.questWindowUI.SetActive(true);
        }
    }

    public void CompleteQuest()
    {
        if (PlayerStats.instance.questIsCompleted)
        {
            Debug.Log("Quest is completed!");
            PlayerStats.instance.quest.isActive = false;
            TakeRequiredItems();
            GiveReward();
        }
    }

    private void TakeRequiredItems()
    {
        for (int i = 0; i < quest.itemRequiredAmount; i++)
        {
            InventoryManager.instance.Remove(quest.requiredItem);
        }
    }

    private void GiveReward()
    {
        for (int i = 0; i < quest.itemRewardAmount; i++)
        {
            InventoryManager.instance.Add(quest.itemReward);
        }

        PlayerStats.instance.money += quest.moneyReward;
        isDone = true;
        PlayerStats.instance.questIsCompleted = false;

        //StartCoroutine("InteractCoroutine");
    }

    //public IEnumerator InteractCoroutine()
    //{
    //    StartCoroutine(TypeSentence(dialogText));
    //    animator.SetBool("IsOpen", true);
    //    ableClick = false;
    //    PlayerStats.instance.isBusy = true;

    //    yield return new WaitForSeconds(4);

    //    ableClick = true;
    //    animator.SetBool("IsOpen", false);
    //    PlayerStats.instance.isBusy = false;
    //}

    //private IEnumerator TypeSentence(string sentence)
    //{
    //    dialogueBoxText.text = "";

    //    foreach (char letter in sentence.ToCharArray())
    //    {
    //        dialogueBoxText.text += letter;
    //        yield return new WaitForSeconds(0.02f);
    //    }
    //}
}
