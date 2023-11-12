using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColector : MonoBehaviour
{
    public  Text candyText;

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
            Destroy(other.gameObject);
            score++;
            PlayerPrefs.SetInt("Score", score);
            candyText.text = score.ToString();
        }
    }
}
