using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : EnemyClass
{
    private void FixedUpdate()
    {
        enemyVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamage(damage);
        }
    }
}
