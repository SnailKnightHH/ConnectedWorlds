using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Move parameters
    [SerializeField] private float movementSpeed = 3f;
    // Attack parameters
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;

    // Jump parameters
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float jumpTime;

    // Wall Jump parameters
    [SerializeField] private float xWallForce;
    [SerializeField] private float yWallForce;
    [SerializeField] private float wallJumpDuration;
    [SerializeField] private float wallSlidingSpeed;

    // Glide Parameters
    [SerializeField] private float glideSpeedX;
    [SerializeField] private float glideSpeedY;

    // Dash Parameters
    [SerializeField] private float dashSpeed;
    [SerializeField] private int dashCountInitial;
    [SerializeField] private float dashDuration;




    // Refernces
    private Rigidbody2D playerRB;
    public Camera cam;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Animations
    public BoxCollider2D LeftBoxCollider;
    public BoxCollider2D RightBoxCollider;

    // move
    private float horizontalInput;
    private float verticalInput;

    // Player state
    public bool grounded = false;
    [SerializeField] bool isFacingRight;
    [SerializeField] bool isTouchingWall;
    [SerializeField] public bool isTouchingLeftWall;
    [SerializeField] public bool isTouchingRightWall;

    // Jump
    private float jumpTimer;
    [SerializeField] private bool isJumping;

    // Attack
    private Vector2 mousePos;
    private Vector2 lookDir;
    private Vector2 fireDir;

    // Health 
    [Range (0, 5)]
    public int health;
    public float invincibleTimeInitial;
    public float invincibleTime;

    // Wall Slide
    [SerializeField] private bool isWallSliding = false;

    // Wall Jump 
    [SerializeField] private bool isWallJumping;

    // Glide
    [SerializeField] private bool isFalling;  

    // Dash
    private int dashCount;
    [SerializeField] private bool isDashing = false;
    [SerializeField] Vector2 dashDirection;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashCount = dashCountInitial;
        invincibleTime = invincibleTimeInitial;
    }

    void Update()
    {
        getInputs();
        updatePlayerState();
        playerDeath();
    }

    private void FixedUpdate()
    {
        HorizontalMove();
        AnimatePlayer();
    }

    private void getInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        GetMousePosition();

        if (isDashing)
        {
            return;
        }
        else if (isWallSliding)
        {
            HorizontalMove();
            if (Input.GetMouseButtonDown(0)) attack();
            WallSliding();
            if (Input.GetKeyDown(KeyCode.Space)) WallJump();
            if (Input.GetKeyDown(KeyCode.LeftShift)) Dash();
        }
        else if (isFalling)
        {
            HorizontalMove();
            if (Input.GetMouseButtonDown(0)) attack();
            if (Input.GetKey(KeyCode.Space)) Glide();
            if (Input.GetKeyDown(KeyCode.LeftShift)) Dash();
        }
        else
        {
            HorizontalMove();
            if (Input.GetMouseButtonDown(0)) attack();
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
            if (Input.GetKey(KeyCode.Space)) JumpHigher();
            if (Input.GetKeyUp(KeyCode.Space)) isJumping = false;
            if (Input.GetKeyDown(KeyCode.LeftShift)) Dash();
        }
        

        //Dash(chargeLeft>0)
        //WallJump(isWallSliding)
        //glide(isFalling)
        //Jump(grounded), JumpHigher(isjumping), movement, 
    }
    private void updatePlayerState()
    {
        isWallSliding = (isTouchingWall && !grounded && horizontalInput != 0);
        if (grounded) DashCountRefresh();
        isFalling = !(grounded || isJumping || isWallSliding);
        isFacingRight = lookDir.x > 0;
        isTouchingWall = isTouchingLeftWall || isTouchingRightWall;
    }



    private void HorizontalMove()
    {
        if(!isDashing && !isWallJumping) { 
        Vector3 targetVelocity = new Vector2(horizontalInput * movementSpeed, playerRB.velocity.y);
        playerRB.velocity = targetVelocity; // horizontal move
        }
    }

    private void AnimatePlayer()
    {
        if (isDashing)
        {
            ChangeAnimationState("character_run");
        }
        else if (isWallSliding)
        {
            ChangeAnimationState("character_wallSlide");
        }
        else if (isFalling)
        {
            ChangeAnimationState("character_jump");
            flipPlayerTransform();
        }
        else if (isJumping || isWallJumping) {
            ChangeAnimationState("character_jump");
            flipPlayerTransform();
        }
        else
        {
            if (horizontalInput == 0) ChangeAnimationState("character_idle");
            else ChangeAnimationState("character_run");
        }
       

        void flipPlayerTransform()
        {
            // flip character sprite
            if (isFacingRight) transform.localScale = new Vector2(1, transform.localScale.y);
            else transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }
    private void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

    private void Jump()
    {
        if (grounded){
            isJumping = true;
            jumpTimer = jumpTime;
            playerRB.velocity = Vector2.up * jumpForce; // first jump
        }
    }
    private void JumpHigher()
    {
        if (jumpTimer > 0 && isJumping)
        {
            playerRB.velocity = Vector2.up * jumpForce; // keep jumping
            jumpTimer -= Time.deltaTime;
        }
        else isJumping = false;
    }

    private void GetMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - (Vector2)transform.position;
        fireDir = mousePos - (Vector2)firePoint.transform.position;
        float FireAngle = Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg - 90f;
        firePoint.transform.eulerAngles = new Vector3(firePoint.transform.rotation.x, firePoint.transform.rotation.y, FireAngle);
    }

    private void attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * bulletForce, ForceMode2D.Impulse);
    }

    private void WallJump()
    {
        isWallJumping = true;
        playerRB.velocity = new Vector3(xWallForce * -horizontalInput, yWallForce, 0); // wall jump
        Invoke("SetIsWallJumpingToFalse", wallJumpDuration);
    }
    void SetIsWallJumpingToFalse()
    {
        isWallJumping = false;
    }

    private void WallSliding()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue)); // wall slide
    }

    private void Glide()
    {
        playerRB.velocity = new Vector2(Mathf.Clamp(playerRB.velocity.x, -glideSpeedX, float.MaxValue),
                                        Mathf.Clamp(playerRB.velocity.y, -glideSpeedY, float.MaxValue)); // glide
    }

    private void Dash()
    {
        if (dashCount-- > 0)
        {
            isDashing = true;
            if(horizontalInput == 0 && verticalInput == 0)
            {
                if (isFacingRight) dashDirection = Vector2.right;
                else dashDirection = Vector2.left;
            }
            else dashDirection = new Vector2(horizontalInput, verticalInput);
            playerRB.velocity = dashDirection.normalized * dashSpeed; // Dash
            Invoke("SetIsDashingToFalse", dashDuration);
        }
    }

    private void DashCountRefresh()
    {
        dashCount = dashCountInitial;
    }

    private void SetIsDashingToFalse()
    {
        isDashing = false;
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
    }

    public void ReceiveDamage (int damageAmount)
    {
        if (invincibleTime > 0)
            invincibleTime -= Time.deltaTime;
        else
        {
            health -= damageAmount;
            invincibleTime = invincibleTimeInitial;
        }
    }

    private void playerDeath()
    {
        if(health <= 0)
        {
            Debug.Log("Failed.");
        }
    }
}

