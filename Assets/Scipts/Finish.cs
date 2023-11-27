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
        }
    }

    private void CompleteLevel() 
    {
        foreach (string name in DontDestroy.dontDestroy.sceneList)
        {
            if (name != "BackgroundAudio")
            {
                Destroy(GameObject.Find(name));
            }
        }
        DontDestroy.dontDestroy.sceneList = new List<string>{"BackgroundAudio"};
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
