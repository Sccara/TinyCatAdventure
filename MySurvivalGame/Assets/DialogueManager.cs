using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one DialogueManager instances");
            return;
        }
        instance = this;
    }

    #endregion

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject player;
    public GameObject dialogueBoxUI;
    public Animator animator;
    private Animator playerAnimator;
    private Queue<string> sentences;
    public bool dialogueIsStarted = false;

    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        playerAnimator.SetFloat("Horizontal", 0);
        playerAnimator.SetFloat("Vertical", 0);
        playerAnimator.SetFloat("Speed", 0);
        playerAnimator.SetFloat("SittingTimer", 0);
        player.GetComponent<AudioSource>().mute = true;
        player.GetComponent<PlayerMovement>().enabled = false;
        animator.SetBool("IsOpen", true);
        PlayerStats.instance.isBusy = true;
        dialogueIsStarted = true;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

    private void EndDialogue()
    {
        StopAllCoroutines();
        player.GetComponent<PlayerMovement>().enabled = true;
        dialogueIsStarted = false;
        PlayerStats.instance.isBusy = false;
        nameText.text = "";
        dialogueText.text = "";
        animator.SetBool("IsOpen", false);
    }    
}
