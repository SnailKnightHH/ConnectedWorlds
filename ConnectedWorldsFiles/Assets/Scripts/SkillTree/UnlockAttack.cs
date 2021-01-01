using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockAttack : SkillTreeClass
{
    public DisplayUnlockAttack displayUnlockAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        theMessage = "Wow my hands are feeling a bit hot... Is it the sun or is it just me?";
        if (collision.tag == "Player")
        {
            //spriteRenderer.enabled = false;
            sceneManager.UnlockAttack();
            displayUnlockAttack.Pause();
            Destroy(gameObject);
        }
    }

}
