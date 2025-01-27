﻿using System;
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
    [SerializeField] public int damage;

    // Damaged effect
     public float flashDuration = 0.1f;

    // References 
    [HideInInspector]
    public Rigidbody2D enemyRB;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    // movement
    private int horizontalMove;
    // health
    private float currentHealth;

    // respawn
    public bool isDead;



    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        horizontalMove = initialDirection;
        currentHealth = MaxHealth;
        isDead = false;
    }


    public void enemyVelocity()
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
        StartCoroutine(PlayDamageEffect());
        if (currentHealth <= 0) isDead = true;
    }

    private IEnumerator PlayDamageEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white;
    }

    public void RespawnHealth()
    {
        currentHealth = MaxHealth;
    }

}
