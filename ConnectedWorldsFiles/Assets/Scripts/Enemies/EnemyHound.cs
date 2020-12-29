using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHound : EnemyClass
{
    private Vector3 playerPos;
    [SerializeField] private Rigidbody2D houndRB;
    [SerializeField] private float attackTimeInitial;
    private float currentAttackTime;

    private void Start()
    {
        currentAttackTime = attackTimeInitial;
    }
    void Update()
    {
        enemyDeath();
        playerPos = player.transform.position;
        if (Vector2.Distance(transform.position, playerPos) < 0.3f && Mathf.Abs(transform.position.y - playerPos.y) < 0.2f) HoundAttack(damage);
        else if (Vector2.Distance(transform.position, playerPos) < 5f && Vector2.Distance(transform.position, playerPos) > 0.3f && Mathf.Abs(transform.position.y - playerPos.y) < 0.2f) detectPlayer();
        else
        {
            houndRB.velocity = new Vector2(0, 0); 
            //movePath();
        }
    }

    private void detectPlayer()
    {
/*        if (player.transform.position.x < transform.position.x)
        {
            flipEnemyTransform(false);
            houndRB.velocity = new Vector2(-1, 0) * movementSpeed;
        }
        else
        {
            flipEnemyTransform(true);
            houndRB.velocity = new Vector2(1, 0) * movementSpeed;
        }*/
    }

    private void HoundAttack(int damage)
    {
        houndRB.velocity = new Vector2(0, 0);
        if (currentAttackTime <= 0)
        {
            player.health -= damage;
            currentAttackTime = attackTimeInitial;
        }
        else
            currentAttackTime -= Time.deltaTime;
            
    }
}
