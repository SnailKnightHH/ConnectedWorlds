﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouchingWall : MonoBehaviour
{

    public PlayerMovement playerMovement;


    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            playerMovement.isTouchingWall = true;
        }
            
        //Debug.Log(collision.gameObject);
        //Debug.Log(playerMovement.isTouchingWall);
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            playerMovement.isTouchingWall = false;
        }
        //Debug.Log(playerMovement.isTouchingWall);
    }
}