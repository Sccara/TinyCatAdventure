using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : ObjectStats
{
    private Enemy enemyScript;

    private void Start()
    {
        enemyScript = GetComponent<Enemy>();
    }

    public override void TakeDamage(float damage)
    {
        enemyScript.Damaged();

        base.TakeDamage(damage);
    }

    public override void Die()
    {
        enemyScript.Destroyed();

        base.Die();
    }
}
