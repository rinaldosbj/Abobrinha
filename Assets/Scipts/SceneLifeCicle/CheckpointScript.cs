using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public AudioSource gotFlagSound;
    private Animator animator;
    new private BoxCollider2D collider2D;
    

    private void Start() 
    {
        animator = gameObject.GetComponent<Animator>();
        collider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player")
        {
            collider2D.enabled = false;
            animator.Play("GotFlag");
            gotFlagSound.Play();
            PersistenceManager.shared.Checkpointed(spawnpoint: transform.position);
        }
    }
}
