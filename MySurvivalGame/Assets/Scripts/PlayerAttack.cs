using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerStats playerStats;

    public Transform attackPoint;

    private float attackRange;

    public LayerMask enemyLayers;
    public LayerMask resourceLayers;

    private void Start()
    {
        playerStats = PlayerStats.instance;
        attackRange = playerStats.attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !playerStats.isAttack && playerStats.stamina >= 20f)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Detect resources in range of attack
        Collider2D[] hitResources = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, resourceLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Damage(enemy);
        }

        foreach (Collider2D resource in hitResources)
        {
            Damage(resource);
        }

        playerStats.UpdateStamina(-20f);

        StartCoroutine("Reload");
    }

    void Damage(Collider2D target)
    {
        target.GetComponent<ObjectStats>().TakeDamage(playerStats.damage.GetValue());

        //if (target.tag == "Enemy")
        //{
        //    Vector2 dir = gameObject.transform.position - target.transform.position;

        //    target.GetComponent<ObjectStats>().TakeDamage(playerStats.damage.GetValue());
        //    target.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * playerStats.attackForce, ForceMode2D.Impulse);
        //}

        //if (target.tag == "MapObject")
        //{
        //    target.GetComponent<ObjectStats>().TakeDamage(playerStats.damage.GetValue());
        //}
    }

    private IEnumerator Reload()
    {
        playerStats.isAttack = true;

        yield return new WaitForSeconds(playerStats.attackRate);

        playerStats.isAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
