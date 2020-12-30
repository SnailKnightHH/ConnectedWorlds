using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private int trapDamage; 

    private void Awake()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player = FindObjectOfType<PlayerController>();
        if (collision.tag == "Player") player.ReceiveDamage(trapDamage);
    }
}
