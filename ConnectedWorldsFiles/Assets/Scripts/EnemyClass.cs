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
    public Rigidbody2D enemyRB;
    public PlayerController player;

    void Start()
    {
        spotNumber = 0;
    }

    public void EnemyReceiveHealth(int damageAmount)
    {
        health -= damageAmount;
    }


    public void movePath()
    {
        enemyRB.position = Vector2.MoveTowards(transform.position, path[spotNumber].position, movementSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, path[spotNumber].position) < 0.2f)
            spotNumber++;
        if (spotNumber == path.Length) spotNumber = 0;
    }

}
