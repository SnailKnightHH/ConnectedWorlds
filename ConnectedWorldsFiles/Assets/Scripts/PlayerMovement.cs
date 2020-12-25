using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Move parameters
    [SerializeField] private float movementSpeed = 3f;
    // Attack parameters
    [SerializeField] private Transform firePoint;
    [SerializeField] private FirePointRotate firePointRotate;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;

    // Jump parameters
    [SerializeField] private float jumpForce = 30f;
    // Charged jump parameters
    [SerializeField] private float chargedJumpForce = 30f;
    [SerializeField] private float chargedJumpMaxTime = 1.5f;
    [SerializeField] private Slider chargeIndicator;

    // Refernces
    private Rigidbody2D playerRB;
    public Camera cam;

    // move
    private float horizontalMovement;
    // Player state
    public bool grounded = false;
    private bool canMove = true;

    // Charged Jump
    private float chargeStartTime;

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
    }

    private void getInputs()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        // Attack
        if (Input.GetMouseButtonDown(0)) attack();
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded) Jump();
        // Charged Jump
        if (Input.GetKeyDown(KeyCode.LeftControl)) ChargingJump();
        if (Input.GetKey(KeyCode.LeftControl)) UpdateIndicator();
        if (Input.GetKeyUp(KeyCode.LeftControl)) ReleaseJump();

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingWall && !grounded) WallJump();
        if (horizontalMovement != 0 && isTouchingWall && !grounded) WallSliding();
        if (facingRight == false && horizontalMovement > 0) Flip();
        else if (facingRight == true && horizontalMovement < 0) Flip();
    }

    private void WallSliding()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }


    private void MovePlayer()
    {
        if (!isWallJumping)
        {
            Vector3 targetVelocity = new Vector2(horizontalMovement * movementSpeed, playerRB.velocity.y);
            playerRB.velocity = targetVelocity;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = frontCheck.localScale;
        scaler.x *= -1;
        frontCheck.localScale = scaler;
        //spriteRenderer.flipX = !spriteRenderer.flipX;

    }

    private void Jump()
    {
        playerRB.AddForce(new Vector2(0, jumpForce));
    }

    private void ChargingJump()
    {
        chargeIndicator.gameObject.SetActive(true);
        chargeIndicator.value = 0f;
        chargeStartTime = Time.time;
    }
    private void UpdateIndicator()
    {
        chargeIndicator.value = Mathf.Min((Time.time - chargeStartTime) / chargedJumpMaxTime, 1f);
    }
    private void ReleaseJump()
    {
        chargeIndicator.gameObject.SetActive(false);
        if (grounded)
        {
            float chargeDuration = Time.time - chargeStartTime;
            playerRB.AddForce(new Vector2(0, Mathf.Min(chargeDuration, chargedJumpMaxTime) * chargedJumpForce));
        }
    }

    private void aiming()
    {
        firePointRotate.firePointAiming();
    }

    private void attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private void WallJump()
    {
        //if(touchWallClass.currentWall == )
        //playerRB.velocity = new Vector2(xWallForce * -1, yWallForce);
        isWallJumping = true;
        if(horizontalMovement > 0)
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
