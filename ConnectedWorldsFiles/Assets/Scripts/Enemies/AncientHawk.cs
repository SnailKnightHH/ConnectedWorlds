using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientHawk : SkyEnemyClass
{
    [SerializeField] private float detectRadius;
    [SerializeField] private float attackInterval;
    private bool isDetect = false;

    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.queriesStartInColliders = false;
        raycastDetect();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!isDetect)
        {
            //enemyRB.velocity = new Vector2(0, 0);
            //movePath();
            //raycastDetect();
            //enemyRB.velocity = new Vector2(0, 0);
        }
            

        

    }

/*    private void RobotAttack()
    {

        if (currentAttackTime <= 0)
        {
            Vector2 shootDir = (player.transform.position - transform.position).normalized;
            float FireAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, FireAngle);
            GameObject Robotbullet = Instantiate(robotbulletPrefab, robotFirePoint.transform.position, robotFirePoint.transform.rotation);
            Rigidbody2D rb = Robotbullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootDir * robotBulletForce, ForceMode2D.Impulse);
            currentAttackTime = attackTimeInitial;
        }
        else
            currentAttackTime -= Time.deltaTime;

    }*/

    private void raycastDetect()
    {
        RaycastHit2D[] hitInfo = Physics2D.CircleCastAll(transform.position, detectRadius, Vector3.forward); //"PlayerPhysics"
        foreach (RaycastHit2D collision in hitInfo)
        {
            Debug.Log(collision.transform.gameObject);
            if (collision.transform.tag == "Player")
            {
                Vector2 playerPos = collision.transform.position;
                Vector2 shootDir = (collision.transform.position - transform.position).normalized;
                float FireAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90;
                transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, FireAngle);
                StartCoroutine(hawkAttack(shootDir));
                isDetect = true;
            }
        }

    }

    private IEnumerator hawkAttack(Vector2 shootDir)
    {
        //enemyRB.MovePosition(playerPos); 
        enemyRB.velocity = new Vector2(shootDir.x, shootDir.y) * movementSpeed;
        yield return new WaitForSeconds(attackInterval);
        //enemyRB.AddForce(dir, ForceMode2D.Impulse);
        //enemyRB.velocity = new Vector2(dir.x, dir.y) * movementSpeed;
        
        isDetect = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
