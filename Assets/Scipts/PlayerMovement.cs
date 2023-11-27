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
    public int jumpQuantity;
    private int jumpCount = 0; 
    private bool isDead = false;
    [SerializeField] private float horizontalHitBoxFixer = .05f; 
    public AudioSource jumpSound;
    public AudioSource doubleJumpSound;
    private bool estaAbaixado = false;
    public float gravityScale = 3f;


    private enum MovementState { idle, walking, jumping, falling, doubleJumping, abaixado, abaixado_down}

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        playerLife = GetComponent<PlayerLife>();
        jumpQuantity = PlayerPrefs.GetInt("Jumps");
        jumpCount = jumpQuantity;
    }

    private void Update()
    {
        if (Input.GetButton("Down"))
        {
            estaAbaixado = true;
            rigidbody.gravityScale = gravityScale * 1.25f;
            float directionX = Input.GetAxisRaw("Horizontal");
            if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                rigidbody.velocity = new Vector2((playerSpeed*directionX)/6,rigidbody.velocity.y);
            }
        }
        else
        {
            rigidbody.gravityScale = gravityScale;
            estaAbaixado = false;
            float directionX = Input.GetAxisRaw("Horizontal");
            if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                rigidbody.velocity = new Vector2(playerSpeed*directionX,rigidbody.velocity.y);
            }
        }

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            if (jumpCount > jumpQuantity-1 || jumpQuantity == 1)
            {
                jumpSound.Play();
            }
            else
            {
                doubleJumpSound.Play();
            }
            isInJumpInteval = false;
            if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x,jumpHeight);
            }
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
            if (jumpCount > 0 || jumpQuantity == 1)
            {
                state = MovementState.jumping;
            }
            else
            {
                state = MovementState.doubleJumping;
            }
        }
        else if (rigidbody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        
        if (estaAbaixado)
        {
            if (rigidbody.velocity.y > -.01f && rigidbody.velocity.y < .01f)
            {
                state = MovementState.abaixado_down;
            }
            else
            {
                state = MovementState.abaixado;
            }
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
            jumpCount = jumpQuantity;
        }
    }
}
