using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockJumpHigher : SkillTreeClass
{

    public DisplayUnlockJumpHigher displayUnlockJumpHigher;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {             
            sceneManager.UnlockHighJump();
            displayUnlockJumpHigher.Pause();
            Destroy(gameObject);
        }
    }
}
