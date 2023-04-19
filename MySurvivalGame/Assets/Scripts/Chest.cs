using System.Collections.Generic;
using UnityEngine;

public class Chest : ObjectStats
{
    public List<GameObject> content;

    public override void Die()
    {
        foreach (var item in content)
        {
            Vector3 offset = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));

            Instantiate(item, transform.position + offset, Quaternion.identity);
        }

        base.Die();
    }
}
