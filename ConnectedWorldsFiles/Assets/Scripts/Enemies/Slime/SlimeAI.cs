using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : EnemyClass
{



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           // collision.GetComponent<PlayerController>. player.ReceiveDamage(damage);
        }
    }
}
