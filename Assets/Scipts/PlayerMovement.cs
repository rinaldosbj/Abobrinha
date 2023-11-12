using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Jobs;

public class PlayerMovement : MonoBehaviour
{
    private float timer = 0;
    public float timeInterval = 2;
    private bool isInJumpInteval = true;
    public float jumpHeight = 0;
    public float minHeight = -2;
    public float playerSpeed = 0;
    new private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    new private BoxCollider2D collider;
    private PlayerLife playerLife;
    public LayerMask jumpableGround;
    public int extraJumpQuantity = 1;
    public int jumpCount = 0; 
    private bool isDead = false;
    [SerializeField] private float horizontalHitBoxFixer = .05f; 

    private enum MovementState { idle, walking, jumping, falling }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        playerLife = GetComponent<PlayerLife>();
        jumpCount = extraJumpQuantity;
    }

    private void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(playerSpeed*directionX,rigidbody.velocity.y);

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isInJumpInteval = false;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x,jumpHeight);
            jumpCount -= 1;
        }
        else
        {
            if (isInJumpInteval)
            {
                givesJumpsBackIfGrounded();
            }
        }

        updateAnimationState();

        if (!isInJumpInteval)
        {
            if (timer < timeInterval)
            { 
                timer += Time.deltaTime; 
            } 
            else 
            {
                timer = 0;
                isInJumpInteval = true;
            }
        }
    }

    private void updateAnimationState()
    {
        MovementState state;

        if (rigidbody.velocity.x > 0f) 
        {
            state = MovementState.walking;
            spriteRenderer.flipX = false;
        }
        else if (rigidbody.velocity.x < 0f) 
        {
            state = MovementState.walking;
            spriteRenderer.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rigidbody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rigidbody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);

        if (!isDead)
        {
            if (transform.position.y <= minHeight)
            {
                isDead = true;
                playerLife.Die();
            }
        }
    }

    private void givesJumpsBackIfGrounded() 
    {   
        Vector3 customSize = new Vector3(collider.bounds.size.x+horizontalHitBoxFixer,collider.bounds.size.y/2,collider.bounds.size.z);
        if (Physics2D.BoxCast(collider.bounds.center, customSize, 0f, Vector2.down, (collider.bounds.size.y/2)+.025f, jumpableGround)){
            jumpCount = extraJumpQuantity;
        }
    }
}
