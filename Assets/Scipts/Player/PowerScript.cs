using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerScript : MonoBehaviour
{
    public bool usedPowerUp = false;
    new private BoxCollider2D collider;
    private Animator animator;
    new private SpriteRenderer renderer;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (usedPowerUp)
        {
            collider.enabled = true;
            renderer.enabled = true;
            animator.enabled = true;
        }
        else {
            collider.enabled = false;
            renderer.enabled = false;
            animator.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Enemy got hitted
            // -1 Life on enemy
            EnemyLife enemyLife = other.gameObject.GetComponent<EnemyLife>();
            enemyLife.gotHitted();
        }
    }

    public void startAnimation()
    {
        animator.Play("Explosion");
    }

    public void stopAnimation()
    {
        animator.Play("Basic_Power");
    }
}
