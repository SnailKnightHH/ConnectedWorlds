using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyClass
{
    private void Update()
    {
        movePath();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.ReceiveDamage(damage);
        }
    }

}
