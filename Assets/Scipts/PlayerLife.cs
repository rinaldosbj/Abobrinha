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
    public  Text lifesText;
    private int lifes;

    private float savedVelocity = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        lifes = PlayerPrefs.GetInt("Life");
        lifesText.text = ($"{lifes}x");
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
        lifes = PlayerPrefs.GetInt("Life");
        PlayerPrefs.SetInt("Life", lifes-1);
    }

    private void RestartLevel()
    {
        if (lifes-1 == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
