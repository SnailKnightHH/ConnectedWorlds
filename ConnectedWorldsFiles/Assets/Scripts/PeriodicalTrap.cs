using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicalTrap : MonoBehaviour
{
    public float trapResetTimeInitial;
    private float trapResetTime;
    [Range (0, 5)]
    public int trapDamage;
    public PlayerMovement player;
    public float attackAvailableTime;
    private bool isAttackAvailable = false;

    // Start is called before the first frame update
    void Start()
    {
        trapResetTime = trapResetTimeInitial;
    }

    // Update is called once per frame
    void Update()
    {
        TrapReset();
        Debug.Log(trapResetTime);
    }

    private void TrapReset()
    {
        if (trapResetTime > 0)
        {
            trapResetTime -= Time.deltaTime;
        }
        else
        {
            Invoke("SetAttackAvailableToTrue", attackAvailableTime);
            trapResetTime = trapResetTimeInitial;
            isAttackAvailable = false;
        }
    }

    private void SetAttackAvailableToTrue()
    {
        isAttackAvailable = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(isAttackAvailable) player.health -= trapDamage;
    }

}
