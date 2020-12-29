using UnityEngine;

public class HoundAI : EnemyClass
{
    private PlayerController player;
    private Vector3 playerPos;
    [SerializeField] private float attackTimeInitial;
    private float currentAttackTime;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentAttackTime = 0;
    }
    void Update()
    {
        playerPos = player.transform.position;
        if (Vector2.Distance(transform.position, playerPos) < 1f && Mathf.Abs(transform.position.y - playerPos.y) < 0.2f) HoundAttack(damage);
        else if (Vector2.Distance(transform.position, playerPos) < 5f && Vector2.Distance(transform.position, playerPos) > 1f && Mathf.Abs(transform.position.y - playerPos.y) < 0.2f) detectPlayer();
        else enemyVelocity();
        Debug.Log(Vector2.Distance(transform.position, playerPos));
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

    public void flipEnemyTransform(bool isFacingRight)
    {
        // flip character sprite
        if (isFacingRight) transform.localScale = new Vector2(transform.localScale.y, transform.localScale.y);
        else transform.localScale = new Vector2(-transform.localScale.y, transform.localScale.y);
    }

    private void HoundAttack(int damage)
    {
        enemyRB.velocity = new Vector2(0, 0);
        if (currentAttackTime <= 0)
        {
            player.remainingHealth -= damage;
            currentAttackTime = attackTimeInitial;
        }
        else
            currentAttackTime -= Time.deltaTime;

    }

}
