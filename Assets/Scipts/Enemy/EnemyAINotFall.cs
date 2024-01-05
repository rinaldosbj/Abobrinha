using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAINotFall : MonoBehaviour
{
    public BoxCollider2D collider2D1;
    public BoxCollider2D collider2D2;
    private float timer = 0;
    private float timeInterval = 0.5f;
    public LayerMask ground;
    private bool isGoingRight = true;
    public float velocity = 0.5f;
    new private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    public bool isTwisted = false;
    private bool canChangeDirection = true;

    private void Awake() 
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canChangeDirection)
        {
            bool rightIsOnTheGround = Physics2D.BoxCast(collider2D2.bounds.center, collider2D2.bounds.size, 0f, Vector2.down, 0f, ground);
            bool leftIsOnTheGround = Physics2D.BoxCast(collider2D1.bounds.center, collider2D1.bounds.size, 0f, Vector2.down, 0f, ground);
            
            if (isTwisted)
            {
                leftIsOnTheGround = Physics2D.BoxCast(collider2D2.bounds.center, collider2D2.bounds.size, 0f, Vector2.down, 0f, ground);
                rightIsOnTheGround = Physics2D.BoxCast(collider2D1.bounds.center, collider2D1.bounds.size, 0f, Vector2.down, 0f, ground);
            }

            if( (rightIsOnTheGround && leftIsOnTheGround ) || (!rightIsOnTheGround && !leftIsOnTheGround))
            {
                // Do nothing
            }
            else if (!rightIsOnTheGround)
            {
                isGoingRight = false;
                canChangeDirection = false;
                spriteRenderer.flipX = true;
            }
            else if (!leftIsOnTheGround)
            {
                isGoingRight = true;
                canChangeDirection = false;
                spriteRenderer.flipX = false;
            }
        }
        else 
        {
            if (timer < timeInterval)
            { 
                timer += Time.deltaTime; 
            } 
            else 
            {
                timer = 0;
                canChangeDirection = true;
            }
        }

        Move();
    }

    private void Move()
    {
        if (isGoingRight)
        {
            rigidbody.velocity = new Vector2(velocity,rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = new Vector2(-velocity,rigidbody.velocity.y);
        }
    } 
}
