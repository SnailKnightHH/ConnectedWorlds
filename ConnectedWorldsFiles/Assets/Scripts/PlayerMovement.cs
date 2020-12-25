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
    public Transform frontCheck;
    public float frontCheckRadius;
    public float wallSlidingSpeed;
    private bool isWallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

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
       // WallSliding(IsWallSliding());
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

    }
    private void MovePlayer()
    {
        if (canMove) { 
        Vector3 targetVelocity = new Vector2(horizontalMovement * movementSpeed, playerRB.velocity.y);
        playerRB.velocity = targetVelocity;
        }
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
        bool isWallSliding = IsWallSliding();
#pragma warning disable CS8321 // Local function is declared but never used
        void SetWallJumpToFalse()
#pragma warning restore CS8321 // Local function is declared but never used
        {
            isWallSliding = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isWallSliding)
        {
            isWallJumping = true;
            Invoke("SetWallJumpToFalse", wallJumpTime);
        }

        if (isWallJumping)
            playerRB.velocity = new Vector2(xWallForce * -horizontalMovement, yWallForce);
    }

    private void Glide()
    {
        if(!grounded && Input.GetKey(KeyCode.Space))
            playerRB.velocity = new Vector2(glidingSpeedComparedToWalkX * horizontalMovement * movementSpeed, glideSpeedY);
    }

}
