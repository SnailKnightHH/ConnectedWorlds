using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRottenRobot : EnemyClass
{
  /*  private Vector3 playerPos;
    [SerializeField] private Rigidbody2D robotRB;


    // Attack
    [SerializeField] private float attackTimeInitial;
    private float currentAttackTime;
    [SerializeField] private GameObject robotFirePoint;
    [SerializeField] private GameObject robotbulletPrefab;
    [SerializeField] private float robotBulletForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if (Vector2.Distance(transform.position, playerPos) < 6f)
        {
            robotRB.velocity = new Vector2(0, 0);
            RobotAttack();
        }
        else if (Vector2.Distance(transform.position, playerPos) < 7.5f && Vector2.Distance(transform.position, playerPos) > 6f) detectPlayer();
        else
        {
            robotRB.velocity = new Vector2(0, 0);
            //movePath();
        }
    }

    private void detectPlayer()
    {
*//*        if (player.transform.position.x < transform.position.x)
        {
            flipEnemyTransform(false);
            robotRB.velocity = new Vector2(-1, 0) * movementSpeed;
        }
        else
        {
            flipEnemyTransform(true);
            robotRB.velocity = new Vector2(1, 0) * movementSpeed;
        }*//*
    }

    private void RobotAttack()
    {
*//*        if (player.transform.position.x < transform.position.x)
        {
            flipEnemyTransform(false);
        }
        else
        {
            flipEnemyTransform(true);
        }*//*

        if (currentAttackTime <= 0)
        {
            Vector2 shootDir = (player.transform.position - robotFirePoint.transform.position).normalized;
            float FireAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90f;
            robotFirePoint.transform.eulerAngles = new Vector3(robotFirePoint.transform.rotation.x, robotFirePoint.transform.rotation.y, FireAngle);
            GameObject Robotbullet = Instantiate(robotbulletPrefab, robotFirePoint.transform.position, robotFirePoint.transform.rotation);
            Rigidbody2D rb = Robotbullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootDir * robotBulletForce, ForceMode2D.Impulse);
            currentAttackTime = attackTimeInitial;
        }
        else
            currentAttackTime -= Time.deltaTime;

    }*/
}
