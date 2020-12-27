using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyClass
{
    private void Update()
    {
        movePath();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.ReceiveDamage(damage);
        }
    }

}
