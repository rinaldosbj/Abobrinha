using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public float timeUntilNextLevel = .5f;
    private bool isLevelCompleted = false;
    public AudioSource finishSound;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player" && !isLevelCompleted)
        {
            Invoke("CompleteLevel",timeUntilNextLevel);
            finishSound.Play();
            isLevelCompleted = true;
            int score = PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("OldScore", score);
        }
    }

    private void CompleteLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
