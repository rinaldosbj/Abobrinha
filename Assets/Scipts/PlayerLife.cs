using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    new private Rigidbody2D rigidbody;
    public AudioSource deadSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Spike"))    
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
