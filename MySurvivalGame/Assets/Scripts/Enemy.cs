using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GameObject player;

    private ObjectStats stats;

    public GameObject lootPrefab;
    public TextMeshProUGUI infoText;
    public Image healthBar;
    public Rigidbody2D rb;

    public string enemyName;
    public float speed = 5f;

    public bool isHited = false;

    Vector2 dir;

    private void Start()
    {
        stats = GetComponent<ObjectStats>();

        infoText.text = enemyName;

        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void Update()
    {
        healthBar.fillAmount = stats.Health / stats.maxHealth;
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (player != null)
        {
            dir = player.transform.position - transform.position;

            if (Vector2.Distance(transform.position, player.transform.position) < 6f && !isHited)
                rb.MovePosition(rb.position + dir.normalized * speed * Time.fixedDeltaTime);
        }        
    }

    public IEnumerator KnockedBack()
    {
        isHited = true;

        yield return new WaitForSeconds(1f);

        isHited = false;

        rb.velocity = new Vector2(0f, 0f);
    }

    public void Damaged()
    {
        StartCoroutine("KnockedBack");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            GameManager.instance.DecreaseColdOverTime(stats.damage.GetValue() * Time.deltaTime);
    }

    public void Destroyed()
    {
        Vector3 offset = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));

        Instantiate(lootPrefab, transform.position + offset, Quaternion.identity);

        Destroy(gameObject);
    }
}
