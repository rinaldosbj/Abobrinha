using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Jobs;

public class PlayerMovement : MonoBehaviour
{
    // Jump control
    private float jumpTimer = 0;
    private float jumpTimeInterval = 0.1f;
    private bool isInJumpInteval = true;
    private float powerTimer = 0;
    public float jumpHeight = 11;
    public LayerMask jumpableGround;
    public int jumpQuantity;
    private int jumpCount = 0;
    public AudioSource jumpSound;
    public AudioSource doubleJumpSound;
    public float gravityScale = 3f;

    // Power
    private float powerTimeInterval = 0.3f;
    private bool isInPowerInterval = true;
    private PowerScript power;
    private bool charged = false;
    public float minHeight = -2;
    public bool hasPowerUp = false;

    // Player atributes
    public float playerSpeed = 0;
    new private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    new private BoxCollider2D collider;
    private PlayerLife playerLife;
    public bool isDead = false;
    [SerializeField] private float horizontalHitBoxFixer = .3f;
    private bool estaAbaixado = false;
    private TrailRenderer trailRenderer;
    private bool querVerBaixo = false;
    private float querVerBaixoTimer = 0;
    private float querVerBaixoTimeInterval = .7f;
    public bool canMove = true;
    private PersistenceManager persistence = PersistenceManager.shared;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        playerLife = GetComponent<PlayerLife>();
        jumpQuantity = persistence.jumpQuantity();
        jumpCount = jumpQuantity;
        power = GetComponentInChildren<PowerScript>();
        hasPowerUp = persistence.hasPowerUp();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetButton("Down"))
            {
                estaAbaixado = true;
                rigidbody.gravityScale = gravityScale * 1.25f;
                float directionX = Input.GetAxisRaw("Horizontal");
                if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
                {
                    rigidbody.velocity = new Vector2((playerSpeed * directionX) / 6, rigidbody.velocity.y);
                }
            }
            else
            {
                rigidbody.gravityScale = gravityScale;
                estaAbaixado = false;
                float directionX = Input.GetAxisRaw("Horizontal");
                if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
                {
                    rigidbody.velocity = new Vector2(playerSpeed * directionX, rigidbody.velocity.y);
                }
            }

            if (Input.GetButtonDown("Jump") && jumpCount > 0)
            {
                if (jumpCount > jumpQuantity - 1 || jumpQuantity == 1)
                {
                    jumpSound.Play();
                }
                else
                {
                    doubleJumpSound.Play();
                    if (hasPowerUp)
                    {
                        charged = true;
                        trailRenderer.emitting = true;
                    }
                }
                isInJumpInteval = false;
                if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHeight);
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
        }
        if (!isInJumpInteval)
        {
            if (jumpTimer < jumpTimeInterval)
            {
                jumpTimer += Time.deltaTime;
            }
            else
            {
                jumpTimer = 0;
                isInJumpInteval = true;
            }
        }
        if (!isInPowerInterval)
        {
            if (powerTimer < powerTimeInterval)
            {
                powerTimer += Time.deltaTime;
            }
            else
            {
                powerTimer = 0;
                isInPowerInterval = true;
                power.usedPowerUp = false;
                power.stopAnimation();
            }
        }
        if (querVerBaixo)
        {
            if (querVerBaixoTimer < querVerBaixoTimeInterval)
            {
                querVerBaixoTimer += Time.deltaTime;
            }
            else
            {
                GameObject.Find("Main Camera").GetComponent<CameraController>().isCrowded = true;
            }
        }
    }

    private void LateUpdate()
    {
        updateAnimationState();
    }

    private void updateAnimationState()
    {
        if (rigidbody.velocity.x > 0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (rigidbody.velocity.x < 0f)
        {
            spriteRenderer.flipX = true;
        }

        if (!isDead)
        {
            if (isDead)
            {

            }
            else if (transform.position.y <= minHeight)
            {
                playerLife.Die();
            }
            else if (estaAbaixado)
            {
                if (rigidbody.velocity.y > -.01f && rigidbody.velocity.y < .01f)
                {
                    animator.Play("Abaixado_down");
                    querVerBaixo = true;
                    if (isInPowerInterval && charged)
                    {
                        if (charged)
                        {
                            isInPowerInterval = false;
                            power.usedPowerUp = true;
                            charged = false;
                            trailRenderer.emitting = false;
                            power.startAnimation();
                        }
                    }
                }
                else
                {
                    if (charged)
                    {
                        animator.Play("Powered_Crowed");
                    }
                    else
                    {
                        animator.Play("Abaixado");
                    }
                }
            }
            else if (rigidbody.velocity.y > .1f)
            {
                if (jumpCount > 0 || jumpQuantity == 1)
                {
                    if (charged)
                    {
                        animator.Play("Powered_Jump");
                    }
                    else
                    {
                        animator.Play("Player_Jump");
                    }
                }
                else
                {
                    if (charged)
                    {
                        animator.Play("Power_DoubleJump");
                    }
                    else
                    {
                        animator.Play("DoubleJump");
                    }
                }
            }
            else if (rigidbody.velocity.y < -.1f)
            {
                if (charged)
                {
                    animator.Play("Powered_Fall");
                }
                else
                {
                    animator.Play("Player_Fall");
                }
            }
            else if (rigidbody.velocity.x > 0f || rigidbody.velocity.x < 0f)
            {
                if (charged)
                {
                    animator.Play("Powered_Walking");
                }
                else
                {
                    animator.Play("Player_Walking");
                }
            }
            else
            {
                if (charged)
                {
                    animator.Play("Powered_Idle");
                }
                else
                {
                    animator.Play("Player_Idle");
                }
            }

            if (!estaAbaixado || (estaAbaixado && !(rigidbody.velocity.y > -.01f && rigidbody.velocity.y < .01f)))
            {
                querVerBaixo = false;
                querVerBaixoTimer = 0;
                GameObject.Find("Main Camera").GetComponent<CameraController>().isCrowded = false;
            }
        }
    }

    private void givesJumpsBackIfGrounded()
    {
        Vector3 customSize = new Vector3(collider.bounds.size.x + horizontalHitBoxFixer, collider.bounds.size.y / 2, collider.bounds.size.z);
        if (Physics2D.BoxCast(collider.bounds.center, customSize, 0f, Vector2.down, (collider.bounds.size.y / 2) + .025f, jumpableGround))
        {
            jumpCount = jumpQuantity;
        }
    }
}
