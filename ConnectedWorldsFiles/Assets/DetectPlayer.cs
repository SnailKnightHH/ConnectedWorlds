using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private HoundAI houndAI;
    [SerializeField] private int damage;

    private void Awake()
    {
        houndAI = GetComponentInParent<HoundAI>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //houndAI.currentAttackTime = 0;//?
            houndAI.isDetected = true;
            Vector2 playerPos = collision.gameObject.transform.position; 

            houndAI.detectPlayer(playerPos);
            
            if (Vector2.Distance(transform.position, playerPos) < 1f 
                && Mathf.Abs(transform.position.y - playerPos.y) < 0.2f)
            {
                houndAI.HoundAttack(damage, collision.gameObject.GetComponent<PlayerController>());
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        houndAI.isDetected = false;
    }


}
