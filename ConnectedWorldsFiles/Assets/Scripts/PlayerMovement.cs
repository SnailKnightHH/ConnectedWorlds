using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public Camera cam;
    public Rigidbody2D playerRB;
    public FirePointRotate firePointRotate;
    [SerializeField] float jumpForce = 30f;
    // Charged jump parameters
    [SerializeField] float chargedJumpForce = 30f;
    [SerializeField] float chargedJumpMaxTime = 1.5f;
    [SerializeField] Slider chargeIndicator;


    private Rigidbody2D rb;
    private float horizontalMovement;
    // Player state
    public bool grounded = false;
    private bool canMove = true;

    // Charged Jump
    private float chargeStartTime;
    


    private bool grounded = false;

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
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        getInputs();
    }

    
    private void FixedUpdate()
    {
        MovePlayer();
        aiming();
        attack();
        WallSliding(IsWallSliding());

    }

    private void getInputs()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
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
        Vector3 targetVelocity = new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
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
            rb.AddForce(new Vector2(0, Mathf.Min(chargeDuration, chargedJumpMaxTime) * chargedJumpForce));
        }
    }

    private void aiming()
    {
        firePointRotate.firePointAiming();
    }

    private void attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            Debug.Log("Fired");
        }
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
        void SetWallJumpToFalse()
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
