using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDash : SkillTreeClass
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        theMessage = "Shift or right click to dash";
        if (collision.tag == "Player")
        {
            sceneManager.canGlide = true;
            sceneManager.UpdateStatus();
            spriteRenderer.enabled = false;
            StartCoroutine(displayMessage());
        }
    }
}
