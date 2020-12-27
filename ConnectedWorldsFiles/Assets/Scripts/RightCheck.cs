using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCheck : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    private Transform target; 
    private string whatIsGround = "WalkableSurface";
    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        target = player.transform;
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
