using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColector : MonoBehaviour
{
    public  Text candyText;
    public  Text lifesText;
    public AudioSource pickUpSound;
    private int score;

    private void Start() 
    {
        score = PlayerPrefs.GetInt("Score");
        candyText.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Candy"))
        {
            other.gameObject.GetComponent<Animator>().Play("CollectSpark");
            score++;
            pickUpSound.Play();
            PlayerPrefs.SetInt("Score", score);
            candyText.text = score.ToString();
        }

        if (other.gameObject.CompareTag("Life"))
        {
            other.gameObject.SetActive(false);
            pickUpSound.Play();
            int lifes = PlayerPrefs.GetInt("Life");
            PlayerPrefs.SetInt("Life", lifes+1);;
            lifesText.text = ($"{lifes+1}x");
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
