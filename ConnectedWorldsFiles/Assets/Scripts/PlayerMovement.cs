using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;

    private Rigidbody2D rb;
    private float horizontalMovement;

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
    }
    private void movePlayer()
    {
        Vector3 targetVelocity = new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
    }
}
