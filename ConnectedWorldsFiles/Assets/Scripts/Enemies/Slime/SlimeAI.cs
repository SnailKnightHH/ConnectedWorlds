using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : EnemyClass
{     
    private Rigidbody2D slimeRB;
    private int horizontalMove;

    private void Awake()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        horizontalMove = 1;
    }

    private void FixedUpdate()
    {
        slimeRB.velocity = new Vector2(horizontalMove * movementSpeed, 0f);
    }

    private void Update()
    {
        enemyDeath();
    }
    public void Changedirection()
    {
        horizontalMove = -horizontalMove;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.ReceiveDamage(damage);
        }
    }
}
