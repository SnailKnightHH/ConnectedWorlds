using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
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
}
