using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Move parameters
    [SerializeField] private float movementSpeed = 3f;

    // Attack parameters
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] private int attackCharge;
    [SerializeField] private float rechargeTimeInitial;

    // Charge UI Parameters
    [SerializeField] public AttackChargeUI attackChargeUI;
    

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
    [SerializeField] public bool dashingDirectionFoward;

    // move
    [SerializeField] private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float horizontalInputRaw;
    private float verticalInputRaw;

    // Player state
    public bool grounded = false;
    [SerializeField] bool isFacingRight;
    [SerializeField] bool isTouchingWall;
    [SerializeField] public bool isTouchingFrontWall;
    [SerializeField] public bool isTouchingBackWall;
    [SerializeField] private bool isFalling;


    // Jump
    [SerializeField] private float jumpTimer;
    [SerializeField] private bool isJumping;

    // Attack
    private Vector2 mousePos;
    private Vector2 lookDir;
    private Vector2 fireDir;
    [SerializeField] private int currentAttackCharge;
    private float rechargeTime;

    // Health 
    [Range(0, 5)]
    public int health;
    public float invincibleTimeInitial;
    public float invincibleTime;
    private bool isInvincible = false;

    // Wall Slide
    [SerializeField] private bool isWallSliding = false;

    // Wall Jump 
    [SerializeField] private bool isWallJumping;

    // Glide
    [SerializeField] private bool isGliding;

    // Dash
    [SerializeField] private int dashCount;
    [SerializeField] public bool isDashing = false;
    [SerializeField] Vector2 dashDirection;

    // Skill Tree
    public bool canJumpHigh = false;
    public bool canAttack = false;
    public bool canWallJump = false;
    public bool canGlide = false;


    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashCount = dashCountInitial;
        invincibleTime = invincibleTimeInitial;
        currentAttackCharge = attackCharge;
        rechargeTime = rechargeTimeInitial;
        // Attack Charge UI 
        attackChargeUI.leftUI.value = 1;
        attackChargeUI.midUI.value = 1;
        attackChargeUI.rightUI.value = 1;
    }

    void Update()
    {
        getInputs();
        UpdatePlayerState();
        InvincibleTime();
        playerDeath();
        if (currentAttackCharge < attackCharge) RechargeAttack();
        RechargeAttackUI();

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
        horizontalInputRaw = Input.GetAxisRaw("Horizontal");
        verticalInputRaw = Input.GetAxisRaw("Vertical");
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
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1)) Dash();
        }
        else if (isFalling)
        {
            HorizontalMove();
            if (Input.GetMouseButtonDown(0)) attack();
            if (Input.GetKey(KeyCode.Space)) Glide();
            if (Input.GetKeyUp(KeyCode.Space)) isGliding = false;
            if (Input.GetKeyDown(KeyCode.LeftShift)) Dash();
        }
        else
        {
            HorizontalMove();
            if (Input.GetMouseButtonDown(0)) attack();
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
            if (Input.GetKey(KeyCode.Space)) JumpHigher();
            if (Input.GetKeyUp(KeyCode.Space)) isJumping = false;
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1)) Dash();
        }


        //Dash(chargeLeft>0)
        //WallJump(isWallSliding)
        //glide(isFalling)
        //Jump(grounded), JumpHigher(isjumping), movement, 
    }
    private void UpdatePlayerState()
    {
        isWallSliding = (isTouchingWall && !grounded && horizontalInput != 0 && canWallJump);
        if (isWallSliding || isDashing) isJumping = false;
        if (grounded && !isDashing) DashCountRefresh();
        isFalling = !(grounded || isJumping || isWallSliding || playerRB.velocity.y >= 0);
        isFacingRight = lookDir.x > 0;
        isTouchingWall = isTouchingFrontWall || isTouchingBackWall;
        if (grounded)  isGliding = false;
        if (canJumpHigh) UnlockJumpHigher(); // to be deleted, changed in scene manager
    }



    private void HorizontalMove()
    {
        if (!isDashing && !isWallJumping)
        {
            Vector3 targetVelocity = new Vector2(horizontalInput * movementSpeed, playerRB.velocity.y);
            playerRB.velocity = targetVelocity; // horizontal move
        }
    }

    private void AnimatePlayer()
    {
        if (isDashing)
        {
            if (dashingDirectionFoward)
                ChangeAnimationState("character_dashFoward");
            else ChangeAnimationState("character_dashBack");
        }
        else if (isJumping)
        {
            ChangeAnimationState("character_jump");
        }
        else if (isWallJumping)
        {
            ChangeAnimationState("character_jump");
        }
        else if (isWallSliding)
        {
            ChangeAnimationState("character_wallSlide");
            if (isTouchingFrontWall) transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        else if (isGliding)
        {
            ChangeAnimationState("character_glide");
            flipPlayerTransform();
        }
        else if (isFalling)
        {
            ChangeAnimationState("character_jump");
            flipPlayerTransform();
        }
        else
        {
            if (horizontalInput == 0) ChangeAnimationState("character_idle");
            else ChangeAnimationState("character_run");
            flipPlayerTransform();
        }
        void flipPlayerTransform()
        {
            // flip character sprite
            if (isFacingRight) transform.localScale = new Vector2(transform.localScale.y, transform.localScale.y);
            else transform.localScale = new Vector2(-transform.localScale.y, transform.localScale.y);
        }
    }
    private void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

    private void Jump()
    {
        if (grounded)
        {
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
        if (canAttack) // Skill Tree Upgrade
        {
            if (currentAttackCharge > 0)
            {
                currentAttackCharge--;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.transform.up * bulletForce, ForceMode2D.Impulse);
            }
        }

    }

    private void RechargeAttack()
    {
        if (rechargeTime <= 0)
        {
            currentAttackCharge++;
            rechargeTime = rechargeTimeInitial;
        }
        else rechargeTime -= Time.deltaTime;
    }

    private void RechargeAttackUI()
    {
        if(currentAttackCharge == 2)
        {
            attackChargeUI.leftUI.value = 1;
            attackChargeUI.midUI.value = 1;
            attackChargeUI.rightUI.value =  1 - rechargeTime;
        }
        if (currentAttackCharge == 1)
        {
            attackChargeUI.leftUI.value = 1;
            attackChargeUI.midUI.value = 1 - rechargeTime;
            attackChargeUI.rightUI.value = 0;
        }
        if (currentAttackCharge == 0)
        {
            attackChargeUI.leftUI.value = 1 - rechargeTime;
            attackChargeUI.midUI.value = 0;
            attackChargeUI.rightUI.value = 0;
        }
    }

    private void WallJump()
    {
        if (canWallJump)
        {
            isWallJumping = true;
            playerRB.velocity = new Vector3(xWallForce * horizontalInputRaw, yWallForce, 0); // wall jump
            Invoke("SetIsWallJumpingToFalse", wallJumpDuration);
        }
    }
    void SetIsWallJumpingToFalse()
    {
        isWallJumping = false;
    }

    private void WallSliding()
    {
        if(canWallJump)
            playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue)); // wall slide
    }

    private void Glide()
    {
        if (canGlide && isFalling)
        {
            isGliding = true;
            playerRB.velocity = new Vector2(Mathf.Clamp(playerRB.velocity.x, -glideSpeedX, float.MaxValue),
                                            Mathf.Clamp(playerRB.velocity.y, -glideSpeedY, float.MaxValue)); 
        }
    }

    private void Dash()
    {
        if (dashCount-- > 0)
        {
            isDashing = true;
            if (horizontalInputRaw == 0 && verticalInputRaw == 0)
            {
                if (isFacingRight) dashDirection = Vector2.right;
                else dashDirection = Vector2.left;
            }
            else dashDirection = new Vector2(horizontalInputRaw, verticalInputRaw);
            dashingDirectionFoward = dashDirection.x >= 0;
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

    public void ReceiveDamage(int damageAmount)
    {
        if (!isInvincible)
        {
            health -= damageAmount;
            invincibleTime = invincibleTimeInitial;
            isInvincible = true;
        }
    }

    private void InvincibleTime()
    {
        if (isInvincible)
            invincibleTime -= Time.deltaTime;
        if (invincibleTime <= 0)
            isInvincible = false;

    }


    private void playerDeath()
    {
        if (health <= 0)
        {
            Debug.Log("Failed.");
        }
    }

    public void UnlockJumpHigher()
    {
        jumpTime = 0.5f;
    }
}

