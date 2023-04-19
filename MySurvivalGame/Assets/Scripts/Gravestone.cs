using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gravestone : ObjectStats
{
    public GameObject skeletonPrefab;

    public override void Die()
    {
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Interactable.ableClick = true;

        yield return new WaitForSeconds(2);

        GameObject gameObject1 = Instantiate(skeletonPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
