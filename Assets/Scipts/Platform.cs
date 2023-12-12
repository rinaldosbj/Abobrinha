using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
            GameObject.Find("Main Camera").GetComponent<CameraController>().isFixed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
            GameObject.Find("Main Camera").GetComponent<CameraController>().isFixed = true;
        }
    }
}
