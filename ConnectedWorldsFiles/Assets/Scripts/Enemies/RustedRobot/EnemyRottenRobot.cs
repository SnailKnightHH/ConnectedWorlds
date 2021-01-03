using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRottenRobot : SkyEnemyClass
{
    private Vector3 playerPos;

    // Attack
    [SerializeField] private float attackTimeInitial;
    private float currentAttackTime;
    [SerializeField] private GameObject robotFirePoint;
    [SerializeField] private GameObject robotbulletPrefab;
    [SerializeField] private float robotBulletForce;
    private GameObject player;

    protected override void Awake()
    {
        currentAttackTime = 0;
        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //player = sceneManager.player;
        player = FindObjectOfType<PlayerController>().gameObject;
        playerPos = player.transform.position;
        if (Vector2.Distance(transform.position, playerPos) < 6f)
        {
            enemyRB.velocity = new Vector2(0, 0);
            RobotAttack();
        }
        else if (Vector2.Distance(transform.position, playerPos) < 7.5f && Vector2.Distance(transform.position, playerPos) > 6f) detectPlayer();
        else
        {
            enemyRB.velocity = new Vector2(0, 0);
            movePath();
        }
    }

    private void detectPlayer()
    {
        if (player.transform.position.x < transform.position.x)
        {
            flipEnemyTransform(false);
            enemyRB.velocity = new Vector2(-1, 0) * movementSpeed;
        }
        else
        {
            flipEnemyTransform(true);
            enemyRB.velocity = new Vector2(1, 0) * movementSpeed;
        }
    }

    private void RobotAttack()
    {
        if (player.transform.position.x < transform.position.x)
        {
            flipEnemyTransform(false);
        }
        else
        {
            flipEnemyTransform(true);
        }

        if (currentAttackTime <= 0)
        {
            Vector2 shootDir = (player.transform.position - robotFirePoint.transform.position).normalized;
            float FireAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90;
            robotFirePoint.transform.eulerAngles = new Vector3(robotFirePoint.transform.rotation.x, robotFirePoint.transform.rotation.y, FireAngle);
            GameObject Robotbullet = Instantiate(robotbulletPrefab, robotFirePoint.transform.position, robotFirePoint.transform.rotation);
            Rigidbody2D rb = Robotbullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootDir * robotBulletForce, ForceMode2D.Impulse);
            currentAttackTime = attackTimeInitial;
        }
        else
            currentAttackTime -= Time.deltaTime;

    }
}
