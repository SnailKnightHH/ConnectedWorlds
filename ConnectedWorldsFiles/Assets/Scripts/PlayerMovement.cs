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
    private bool canMove = true;

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
   // public IsTouchingWall touchWallClass;
    public float wallHopForce;
    private int facingDirection = 1;
    public float wallSlidingSpeed;
    private bool isWallJumping = false;
    public float JumpTime;
    public bool isFlipping;
    private bool facingRight = false;
    public Transform frontCheck; 

    // Glide
    [Range(0, 1)]
    public float glidingSpeedComparedToWalkX;
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
        MovePlayer();
        aiming();
        // playerRB.AddForce(new Vector2(xWallForce, yWallForce), ForceMode2D.Impulse); 
        //playerRB.velocity = new Vector2(xWallForce * -horizontalMovement, yWallForce);
        //playerRB.AddForce(new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y), ForceMode2D.Impulse);
        AnimatePlayer();
    }

    private void getInputs()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        // Attack
        GetMousePosition();
        if (Input.GetMouseButtonDown(0)) attack();
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded) Jump();
        if (Input.GetKey(KeyCode.Space)) JumpHigher();
        if (Input.GetKeyUp(KeyCode.Space)) isJumping = false;
        if (Input.GetKeyDown(KeyCode.Space) && isTouchingWall && !grounded) WallJump();
        if (horizontalMovement != 0 && isTouchingWall && !grounded) WallSliding();
    }

    private void WallSliding()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }


    private void MovePlayer()
    {
        if (canMove) {
        Vector3 targetVelocity = new Vector2(horizontalMovement * movementSpeed, playerRB.velocity.y);
        playerRB.velocity = targetVelocity;
            //flip character sprite
            if (lookDir.x < 0) transform.localScale = new Vector2 (-1, transform.localScale.y);
            else if (lookDir.x >= 0) transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    private void AnimatePlayer()
    {
        if (grounded) { 
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
        lookDir = mousePos - (Vector2) transform.position;
        fireDir = mousePos - (Vector2) firePoint.transform.position;
        float FireAngle = Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg - 90f;
        firePoint.transform.eulerAngles = new Vector3(firePoint.transform.rotation.x, firePoint.transform.rotation.y, FireAngle);
    }

    private void attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * bulletForce, ForceMode2D.Impulse);
    }

    private bool IsWallSliding()
    {
        Collider2D checkTouchingWall;
        checkTouchingWall = Physics2D.OverlapCircle(frontCheck.position, frontCheckRadius);
        if (checkTouchingWall.gameObject.CompareTag("Wall") && grounded == false)
            return true;
        else
            return false;
    }

    private void WallSliding(bool canWallSlide)
    {
        if (canWallSlide)
            playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }

    private void WallJump()
    {
        //if(touchWallClass.currentWall == )
        //playerRB.velocity = new Vector2(xWallForce * -1, yWallForce);
        isWallJumping = true;
        playerRB.velocity = new Vector3(xWallForce * -horizontalMovement, yWallForce, 0);
        Invoke("SetIsWallJumpingToFalse", JumpTime);
        Debug.Log("Reached");
        //isWallJumping = false;
        /*        Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
                playerRB.AddForce(forceToAdd, ForceMode2D.Impulse);
                wallJumpTime = wallJumpTimeStart;*/
        //Debug.Log(playerRB.velocity.x);
        /*        if (wallJumpTime <= 0)
                {

                }
                else
                {
                    wallJumpTime -= Time.deltaTime;
                }*/
    }

    void SetIsWallJumpingToFalse()
    {
        isWallJumping = false;
    }

}
