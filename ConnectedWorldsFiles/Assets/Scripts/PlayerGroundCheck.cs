using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            playerMovement.grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            playerMovement.grounded = false;
        }
    }
}
