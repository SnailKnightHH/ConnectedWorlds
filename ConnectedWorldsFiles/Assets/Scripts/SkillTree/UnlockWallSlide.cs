using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWallSlide : SkillTreeClass
{
    public DisplayUnlockWallJump displayUnlockWallJump;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        theMessage = "The walls feel different... Can I now move like a ninja?";
        if (collision.tag == "Player")
        {
            sceneManager.UnlockWallJump();
            displayUnlockWallJump.Pause();
            Destroy(gameObject);
        }
    }

}
