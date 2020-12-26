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

    // Refernces
    private Rigidbody2D playerRB;
    public Camera cam;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Animations

    // move
    private float horizontalMovement;
    // Player state
    public bool grounded = false;

    // Jump
    private float jumpTimer;
    private bool isJumping;

    // Attack
    Vector2 mousePos;
    Vector2 lookDir;
    Vector2 fireDir;

    // Wall Jump 
    public bool isTouchingWall;
    public float xWallForce;
    public float yWallForce;
    public float wallHopForce;
    public float wallSlidingSpeed;
    private bool isWallJumping = false;
    public float wallJumpTime;
    public bool isFlipping;
    public Transform frontCheck;

    // Glide
    public float glideSpeedX;
    public float glideSpeedY;


    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        getInputs();
    }


    private void FixedUpdate()
    {
        if (!isWallJumping) MovePlayer();
        AnimatePlayer();
    }

    private void getInputs()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        // Attack
        GetMousePosition();
        if (Input.GetMouseButtonDown(0)) attack();
        // Jump
        if (Input.GetKeyUp(KeyCode.Space)) isJumping = false;
        if (Input.GetKeyDown(KeyCode.Space) && isTouchingWall && !grounded) WallJump();
        if (horizontalMovement != 0 && isTouchingWall && !grounded) WallSliding();
        if (!isWallJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded) Jump();
            if (Input.GetKey(KeyCode.Space)) JumpHigher();
            if (!grounded && !isTouchingWall && Input.GetKey(KeyCode.Space)) Glide();
        }
    }

    private void WallSliding()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }


    private void MovePlayer()
    {
        Vector3 targetVelocity = new Vector2(horizontalMovement * movementSpeed, playerRB.velocity.y);
        playerRB.velocity = targetVelocity;

        //flip character sprite
        if (lookDir.x < 0) transform.localScale = new Vector2(-1, transform.localScale.y);
        else if (lookDir.x >= 0) transform.localScale = new Vector2(1, transform.localScale.y);
    }

    private void AnimatePlayer()
    {
        if (grounded)
        {
            if (horizontalMovement == 0) ChangeAnimationState("character_idle");
            else ChangeAnimationState("character_run");
        }
        else
        {
            ChangeAnimationState("character_jump");
        }
    }

    private void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

    private void Jump()
    {
        isJumping = true;
        jumpTimer = jumpTime;
        playerRB.velocity = Vector2.up * jumpForce;
    }
    private void JumpHigher()
    {
        if (jumpTimer > 0 && isJumping)
        {
            playerRB.velocity = Vector2.up * jumpForce;
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
        playerRB.velocity = new Vector3(xWallForce * -horizontalMovement, yWallForce, 0);
        Invoke("SetIsWallJumpingToFalse", wallJumpTime);
    }

    void SetIsWallJumpingToFalse()
    {
        isWallJumping = false;
    }

    private void Glide()
    {
        playerRB.velocity = new Vector2(Mathf.Clamp(playerRB.velocity.x, -glideSpeedX, float.MaxValue),
                                        Mathf.Clamp(playerRB.velocity.y, -glideSpeedY, float.MaxValue));
    }


}
