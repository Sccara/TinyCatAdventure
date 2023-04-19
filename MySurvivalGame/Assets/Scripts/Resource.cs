using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Resource : ObjectStats
{
    public GameObject lootPrefab;


    public override void Die()
    {
        Vector3 offset = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));

        Instantiate(lootPrefab, transform.position + offset, Quaternion.identity);

        base.Die();
    }
}
