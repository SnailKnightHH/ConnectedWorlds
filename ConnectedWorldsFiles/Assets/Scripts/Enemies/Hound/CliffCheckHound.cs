using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffCheckHound : MonoBehaviour
{
    private HoundAI houndAI;
    private string whatIsGround = "WalkableSurface";
    private void Awake()
    {
        houndAI = GetComponentInParent<HoundAI>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            houndAI.Changedirection();
        }
    }
}
