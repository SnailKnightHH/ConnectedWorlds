using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffCheck : MonoBehaviour
{
    private SlimeAI slimeAI;
    private string whatIsGround = "WalkableSurface";
    private void Awake()
    {
        slimeAI = GetComponentInParent<SlimeAI>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            slimeAI.ChangeDirection();
        }
    }
}
