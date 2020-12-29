using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyEnemyClass : MonoBehaviour
{
    // This class is for sky based enemies with way points patrol system


    // Parameters
    [SerializeField] private int MaxHealth;
    [SerializeField] public float movementSpeed;

    // Damaged effect
    [SerializeField] private float flashDuration = 0.1f;

    // References 
    public SceneManager sceneManager;
    [HideInInspector]
    public Rigidbody2D enemyRB;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    // health
    private float currentHealth;

    // Movement
    private int currentSpot;
    [SerializeField] private Transform[] wayPoints;


    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = MaxHealth;
        sceneManager = FindObjectOfType<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpot = 0;
    }

    public void movePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentSpot].position, movementSpeed * Time.deltaTime);

        // Flip enemy
        if (transform.position.x < wayPoints[currentSpot].position.x)
            flipEnemyTransform(true);
        else
            flipEnemyTransform(false);

        if (Vector2.Distance(transform.position, wayPoints[currentSpot].position) < 0.2f)
            currentSpot++;
        if (currentSpot == wayPoints.Length) currentSpot = 0;
    }

    public void flipEnemyTransform(bool isFacingRight)
    {
        // flip character sprite
        if (isFacingRight) transform.localScale = new Vector2(transform.localScale.y, transform.localScale.y);
        else transform.localScale = new Vector2(-transform.localScale.y, transform.localScale.y);
    }

    public void ReceiveDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        StartCoroutine(PlayDamageEffect());
        if (currentHealth <= 0) Destroy(gameObject);
    }

    private IEnumerator PlayDamageEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white;
    }
}
