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
            other.gameObject.SetActive(false);
            pickUpSound.Play();
            persistenceManager.lifeUP();
        }

        if (other.gameObject.CompareTag("Power"))
        {
            other.gameObject.SetActive(false);
            pickUpSound.Play();
            persistenceManager.gotPowerUp();
        }

        if (other.gameObject.CompareTag("DoubleJump"))
        {
            other.gameObject.GetComponent<Animator>().Play("DoubleJump_Destroy");
            pickUpSound.Play();
            persistenceManager.jumpUP();
        }
    }
}
