using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private bool isGoingUp = true;
    private float timer = 0;
    public float timeInterval = 2;

    public float travelDistance = 100;
    public float sizeScaler = .0001f;
    private void Update() 
    {
        if (Input.anyKeyDown)
        {
            StartGame();
            PersistenceManager.shared.resetData();
        }
        if (timer < timeInterval)
        { 
            timer += Time.deltaTime; 
        } 
        else 
        {
            timer = 0;
            moveInDifferentDirection();
        }
        if (isGoingUp) 
        {
            transform.position = transform.position + (Vector3.up * travelDistance) * Time.deltaTime;
            transform.localScale += new Vector3(sizeScaler, sizeScaler, 0);        
        }
        else 
        {
            transform.position = transform.position + (Vector3.down * travelDistance) * Time.deltaTime;
            transform.localScale -= new Vector3(sizeScaler, sizeScaler, 0);        
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void moveInDifferentDirection()
    {
        if (isGoingUp) {
            isGoingUp = false;
        } else {
            isGoingUp = true;
        }
    }
}
