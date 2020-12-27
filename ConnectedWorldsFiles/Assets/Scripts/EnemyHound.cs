using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHound : EnemyClass
{
    [SerializeField] private float detectRange;
    private Vector3 playerPos;

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if (Vector2.Distance(transform.position, playerPos) < 0.2f && (transform.position.y - playerPos.y) < 0.1f) HoundAttack(damage);
        if (Vector2.Distance(transform.position, playerPos) < 5f && (transform.position.y - playerPos.y) < 0.1f) detectPlayer(detectRange);
        else movePath();
    }

    private void detectPlayer(float detectRange)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    private void HoundAttack(int damage)
    {
        player.health -= damage;
    }
}
