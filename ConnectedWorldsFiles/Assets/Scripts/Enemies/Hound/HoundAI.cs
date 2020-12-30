using UnityEngine;

public class HoundAI : EnemyClass
{
    [SerializeField] private float attackTimeInitial;
    private float currentAttackTime;
    public bool isDetected = false;

    private void Start()
    {
        currentAttackTime = 0;
    }
    void Update()
    {
        if(!isDetected)
            enemyVelocity();
    }

    public void detectPlayer(Vector2 playerPos)
    {
        if (playerPos.x < transform.position.x)
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

    public void HoundAttack(int damage, PlayerController player)
    {
        enemyRB.velocity = new Vector2(0, 0);
        if (currentAttackTime <= 0)
        {
            player.ReceiveDamage(damage);
            currentAttackTime = attackTimeInitial;
        }
        else
            currentAttackTime -= Time.deltaTime;
    }

}
