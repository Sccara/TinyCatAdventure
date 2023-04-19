using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float idleTimer = 0.0f;
    public bool isRunning;

    [HideInInspector]
    public float startSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Transform attackPoint;

    private PlayerStats playerStats;

    public Vector2 movement;
    

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Footsteps");
        playerStats = GetComponent<PlayerStats>();
        speed = startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0)
        {
            GetComponent<AudioSource>().mute = false;
        }
        else
        {
            GetComponent<AudioSource>().mute = true;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0)
        {
            animator.SetBool("IsFlip", true);
        }
        else if(movement.x < 0)
        {
            animator.SetBool("IsFlip", false);
        }

        if (animator.GetFloat("Speed") > 0 || idleTimer >= 35)
        {
            idleTimer = 0.0f;
        }
        else
        {
            idleTimer += Time.deltaTime;
            animator.SetFloat("SittingTimer", idleTimer);
        }

        //MoveAttackPoint();

        if (Input.GetKey(KeyCode.LeftShift) && playerStats.stamina > 0)
        {
            GetComponent<AudioSource>().pitch = 1.5f;
            speed = startSpeed * 2;
            animator.speed = 2;
            playerStats.UpdateStamina(-1f * Time.deltaTime);
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || playerStats.stamina <= 0)
        {
            GetComponent<AudioSource>().pitch = 1f;
            speed = startSpeed;
            animator.speed = 1;
            isRunning = false;
        }

        if (playerStats.stamina <= 0)
        {
            GetComponent<AudioSource>().pitch = 1f;
            speed = startSpeed;
            animator.speed = 1;
            isRunning = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    void MoveAttackPoint()
    {
        if (movement.x > 0)
        {
            attackPoint.position = gameObject.transform.position + new Vector3(0.5f, 0f, 0f);
        }
        if (movement.x < 0)
        {
            attackPoint.position = gameObject.transform.position + new Vector3(-0.5f, 0f, 0f);
        }
        if (movement.y > 0)
        {
            attackPoint.position = gameObject.transform.position + new Vector3(0f, 0.5f, 0f);
        }
        if (movement.y < 0)
        {
            attackPoint.position = gameObject.transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}
