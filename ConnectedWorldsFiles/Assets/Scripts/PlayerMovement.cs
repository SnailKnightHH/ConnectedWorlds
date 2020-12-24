using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public Camera cam;
    public Rigidbody2D playerRB;
    public FirePointRotate firePointRotate;

    private Rigidbody2D rb;
    private float horizontalMovement;


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
        movePlayer();
        aiming();
        attack();
        WallSliding(IsWallSliding());
    }

    private void getInputs()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }
    private void movePlayer()
    {
        Vector3 targetVelocity = new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
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
