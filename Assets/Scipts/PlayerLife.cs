using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
        if (other.gameObject.CompareTag("Hog"))    
        {
            Die();
        }
    }

    public void Die()
    {
        deadSound.Play();
        animator.SetTrigger("death");
        rigidbody.bodyType = RigidbodyType2D.Static;
        int oldScore = PlayerPrefs.GetInt("OldScore");
        PlayerPrefs.SetInt("Score", oldScore);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
