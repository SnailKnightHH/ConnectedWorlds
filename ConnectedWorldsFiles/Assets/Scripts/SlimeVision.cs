using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeVision : MonoBehaviour
{
    private SlimeAI slimeAI;
    private string whatIsGround = "WalkableSurface";
    private void Awake()
    {
        slimeAI = GetComponentInParent<SlimeAI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround) || collision.tag == "Slime")
        {
            slimeAI.Changedirection();
        }
    }
}
