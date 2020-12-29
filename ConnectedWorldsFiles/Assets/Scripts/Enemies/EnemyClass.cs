using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    // This class is for ground based enemies with standard patrol movement 


    // Parameters
    [SerializeField] private int MaxHealth;
    [SerializeField] public float movementSpeed;
    [SerializeField] [Range(-1, 1)] int initialDirection = 1;

    // References 
    public Rigidbody2D enemyRB;
    // movement
    private int horizontalMove;
    // health
    private float currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        horizontalMove = initialDirection;
    }

    private void FixedUpdate()
    {
        enemyRB.velocity = new Vector2(horizontalMove * movementSpeed, 0f);
    }

    public void ChangeDirection()
    {
        horizontalMove = -horizontalMove;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    public void ReceiveDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamage();
        }
    }
}
