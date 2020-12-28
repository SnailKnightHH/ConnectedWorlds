using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRottenRobot : EnemyClass
{
    [SerializeField] private float detectRange;
    private Vector3 playerPos;
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
        enemyDeath();
        playerPos = player.transform.position;
        if (Vector2.Distance(transform.position, playerPos) < 4f) RobotAttack();
        else if (Vector2.Distance(transform.position, playerPos) < 6f && Vector2.Distance(transform.position, playerPos) > 4f) detectPlayer(detectRange);
        else
        {
            robotRB.velocity = new Vector2(0, 0);
            movePath();
        }
    }

    private void detectPlayer(float detectRange)
    {
        if (player.transform.position.x < transform.position.x)
            robotRB.velocity = new Vector2(-1, 0) * movementSpeed;
        else
            robotRB.velocity = new Vector2(1, 0) * movementSpeed;
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    private void RobotAttack()
    {
        if (currentAttackTime <= 0)
        {
            GameObject Robotbullet = Instantiate(robotbulletPrefab, robotFirePoint.transform.position, robotFirePoint.transform.rotation);
            Rigidbody2D rb = Robotbullet.GetComponent<Rigidbody2D>();
            Vector2 shootDir = (player.transform.position - robotFirePoint.transform.position).normalized;
            rb.AddForce(shootDir * robotBulletForce, ForceMode2D.Impulse);
            currentAttackTime = attackTimeInitial;
        }
        else
            currentAttackTime -= Time.deltaTime;

    }
}
