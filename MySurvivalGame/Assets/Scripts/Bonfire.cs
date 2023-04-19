using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private GameObject player;

    public Animator animator;

    private float dist;
    private float recoveryRate = 10f;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    public void Update()
    {
        dist = (player.transform.position - transform.position).magnitude;

        if (Input.GetKeyDown(KeyCode.E) && dist < 2)
        {
            Interact();
        }

        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 2f)
                RegenPlayerStats();
        }
    }

    private void Interact()
    {
        animator.SetBool("IsFired", true);

        // Set spawnpoint to bonfire
    }

    void RegenPlayerStats()
    {
        player.GetComponent<PlayerStats>().UpdateHealth(recoveryRate * Time.deltaTime);
        player.GetComponent<PlayerStats>().UpdateStamina(recoveryRate * Time.deltaTime);
        player.GetComponent<PlayerStats>().UpdateCold(recoveryRate * Time.deltaTime);
    }
}
