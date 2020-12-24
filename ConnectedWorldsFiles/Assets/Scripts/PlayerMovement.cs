using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float jumpForce = 30f;


    private Rigidbody2D rb;
    private float horizontalMovement;
    public bool canJump = false;

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
    }

    private void getInputs()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        // Jump
        if (Input.GetKeyDown("space") && canJump) jump();
    }
    private void movePlayer()
    {
        Vector3 targetVelocity = new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
    }

    private void jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }
}
