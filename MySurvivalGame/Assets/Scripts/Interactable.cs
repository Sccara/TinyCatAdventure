using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public TextMeshProUGUI dialogueBoxText;
    public string dialogText;
    private GameObject closest;
    [SerializeField]
    public static bool ableClick = true;
    private static float closestDist = 2;
    public float dist;

    public void Update()
    {
        closest = GameManager.instance.FindClosestInteractableObject(2);

        if (closest != null)
        {
            closestDist = (closest.transform.position - player.transform.position).magnitude;
        }
        
        dist = (transform.position - player.transform.position).magnitude;

        if (Input.GetKeyDown(KeyCode.E) && dist <= closestDist && ableClick && !PlayerStats.instance.isBusy)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        StartCoroutine("InteractCoroutine");
    }

    public IEnumerator InteractCoroutine()
    {
        StartCoroutine(TypeSentence(dialogText));
        animator.SetBool("IsOpen", true);
        ableClick = false;
        PlayerStats.instance.isBusy = true;

        yield return new WaitForSeconds(4);

        ableClick = true;
        animator.SetBool("IsOpen", false);
        PlayerStats.instance.isBusy = false;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueBoxText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueBoxText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
