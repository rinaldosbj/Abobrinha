using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColector : MonoBehaviour
{
    public AudioSource pickUpSound;
    private PersistenceManager persistenceManager = PersistenceManager.shared;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Candy"))
        {
            other.gameObject.GetComponent<Animator>().Play("CollectSpark");
            pickUpSound.Play();
            persistenceManager.scoreUP();
        }

        if (other.gameObject.CompareTag("Life"))
        {
            // TEMP
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //
            pickUpSound.Play();
            persistenceManager.lifeUP();
        }

        if (other.gameObject.CompareTag("Power"))
        {
            // TEMP
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //
            pickUpSound.Play();
            persistenceManager.gotPowerUp();
            gameObject.GetComponent<PlayerMovement>().UpdatePlayer();
        }

        if (other.gameObject.CompareTag("DoubleJump"))
        {
            other.gameObject.GetComponent<Animator>().Play("DoubleJump_Destroy");
            pickUpSound.Play();
            persistenceManager.jumpUP();
            gameObject.GetComponent<PlayerMovement>().UpdatePlayer();
        }
    }
}
