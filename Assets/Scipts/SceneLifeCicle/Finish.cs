using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public float timeUntilNextLevel = .5f;
    private bool isLevelCompleted = false;
    public AudioSource finishSound;
    private Animator animator;

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player" && !isLevelCompleted)
        {
            other.GetComponent<PlayerMovement>().canMove = false;
            animator.Play("SoulMagic");
            Invoke("CompleteLevel",timeUntilNextLevel);
            finishSound.Play();
            isLevelCompleted = true;
        }
    }

    private void CompleteLevel() 
    {
        foreach (string name in DontDestroy.shared.sceneList)
        {
            if (name != "Manager")
            {
                Debug.Log($"Destroyed CompleteLevel -> {name}");
                Destroy(GameObject.Find(name));
            }
        }
        PersistenceManager.shared.RestartToNextLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
