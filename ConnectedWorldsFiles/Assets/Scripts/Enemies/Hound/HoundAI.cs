using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundAI : EnemyClass
{
    private Rigidbody2D houndRB;
    private int horizontalMove;

    // Start is called before the first frame update
    void Start()
    {
        houndRB = GetComponent<Rigidbody2D>();
        horizontalMove = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        houndRB.velocity = new Vector2(horizontalMove * movementSpeed, 0f);
    }

    public void Changedirection()
    {
        horizontalMove = -horizontalMove;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
