using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private int trapDamage; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") player.ReceiveDamage();
    }
}
