using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public int health;
    [Range (0, 5)]
    public int damage;
    public float movementSpeed;
    public Transform[] path;
    private int spotNumber;
    public PlayerController player;

    void Start()
    {
        spotNumber = 0;
    }

    public void EnemyReceiveDamage(int damageAmount)
    {
        health -= damageAmount;
    }


    public void movePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[spotNumber].position, movementSpeed * Time.deltaTime);

        // Flip enemy
        if (transform.position.x < path[spotNumber].position.x)
            flipEnemyTransform(true);
        else
            flipEnemyTransform(false);

        if (Vector2.Distance(transform.position, path[spotNumber].position) < 0.2f)
            spotNumber++;
        if (spotNumber == path.Length) spotNumber = 0;
    }

    public void enemyDeath()
    {
        if (health <= 0) Destroy(gameObject);
    }

    public void flipEnemyTransform(bool isFacingRight)
    {
        // flip character sprite
        if (isFacingRight) transform.localScale = new Vector2(transform.localScale.y, transform.localScale.y);
        else transform.localScale = new Vector2(-transform.localScale.y, transform.localScale.y);
    }

}
