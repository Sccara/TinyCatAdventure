using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    public Dialogue dialogue;
    public bool isInteracted = false;

    public override void Interact()
    {
        // Maybe add some parametres to control the messages needed to display, StartDialogue(dialogue, start, end)
        // This variables will change from the other part of code in different cases, like u already talked to this character
        if (!GetComponent<QuestGiver>().quest.isActive && !GetComponent<QuestGiver>().isDone)
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }

        isInteracted = true;
    }
}
