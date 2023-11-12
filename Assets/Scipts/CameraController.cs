using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float maxYposition = 0;

    private void Update()
    {
        float positionY = player.position.y;
        if (positionY < maxYposition)
        {
            positionY = maxYposition;
        }
        transform.position = new Vector3(player.position.x, positionY, transform.position.z);
    }
}
