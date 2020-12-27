using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCheck : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController playerMovement;
    private string whatIsGround = "WalkableSurface";
    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            playerMovement.isTouchingBackWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            playerMovement.isTouchingBackWall = false;
        }
    }
}
