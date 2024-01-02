using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColector : MonoBehaviour
{
    public AudioSource pickUpSound;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Candy"))
        {
            other.gameObject.GetComponent<Animator>().Play("CollectSpark");
            pickUpSound.Play();
            PersistenceManager.persistenceManager.scoreUP();
        }

        if (other.gameObject.CompareTag("Life"))
        {
            other.gameObject.SetActive(false);
            pickUpSound.Play();
            PersistenceManager.persistenceManager.lifeUP();
        }

        if (other.gameObject.CompareTag("Power"))
        {
            other.gameObject.SetActive(false);
            pickUpSound.Play();
            PlayerPrefs.SetInt("hasPowerUp",1);
            GetComponentInParent<PlayerMovement>().hasPowerUp = true;
        }

        if (other.gameObject.CompareTag("DoubleJump"))
        {
            other.gameObject.GetComponent<Animator>().Play("DoubleJump_Destroy");
            pickUpSound.Play();
            PlayerPrefs.SetInt("Jumps",2);
            GameObject.Find("Player").GetComponent<PlayerMovement>().jumpQuantity = 2;
        }
    }
}
