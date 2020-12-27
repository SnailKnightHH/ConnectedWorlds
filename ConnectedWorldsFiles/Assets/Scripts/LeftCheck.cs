using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCheck : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    private string whatIsGround = "WalkableSurface";
    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            playerMovement.isTouchingFrontWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            playerMovement.isTouchingFrontWall = false;
        }
    }
}
