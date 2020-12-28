using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWallSlide : SkillTreeClass
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        theMessage = "The walls feel different... Can I now move like a ninja?";
        if (collision.tag == "Player")
        {
            player.canWallJump = true;
            spriteRenderer.enabled = false;
            StartCoroutine(displayMessage());
        }
    }
}
