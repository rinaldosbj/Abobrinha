using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerScript : MonoBehaviour
{
    public bool usedPowerUp = false;
    new private BoxCollider2D collider;
    new private SpriteRenderer renderer;
    private Rigidbody2D playerBody;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        playerBody = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        if (usedPowerUp)
        {
            collider.enabled = true;
            renderer.enabled = true;
        }
        else {
            collider.enabled = false;
            renderer.enabled = false;
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
}
