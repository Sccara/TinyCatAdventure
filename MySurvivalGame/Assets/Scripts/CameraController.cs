using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
