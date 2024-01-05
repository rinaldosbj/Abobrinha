using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    new private Rigidbody2D rigidbody;
    public AudioSource deadSound;

    private float savedVelocity = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        savedVelocity = rigidbody.velocity.y;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike") && savedVelocity < -.1f)
        {
            Die();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.name == "Hog")
            {
                Die();
            }
        }
    }

    public void Die()
    {
        GetComponent<PlayerMovement>().isDead = true;
        deadSound.Play();
        animator.SetTrigger("death");
        rigidbody.bodyType = RigidbodyType2D.Static;
        PersistenceManager.shared.LifeDOWN();
        PersistenceManager.shared.UpdateScore();
    }

    private void RestartLevel()
    {
        if (PersistenceManager.shared.lifes() == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            foreach (string name in DontDestroy.shared.sceneList)
            {
                if (!CheckIfMustPersist(name))
                {
                    Destroy(GameObject.Find(name));
                }
            }
            DontDestroy.shared.sceneList = new List<string>();
            foreach (string name in PersistenceManager.shared.dontDestroyPreviousSceneList)
            {
                DontDestroy.shared.sceneList.Add(name.ToString());
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public bool CheckIfMustPersist(string name){
        GameObject gameObjectFromName = GameObject.Find(name);
        if (gameObjectFromName == null){
            return false;
        }
        return gameObjectFromName.CompareTag("Undestroyable") || gameObjectFromName.CompareTag("Life") || gameObjectFromName.CompareTag("Power") || gameObjectFromName.CompareTag("DoubleJump");
    }
}
