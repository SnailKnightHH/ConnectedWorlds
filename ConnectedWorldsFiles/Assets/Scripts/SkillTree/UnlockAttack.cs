using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockAttack : SkillTreeClass
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        theMessage = "Wow my hands are feeling a bit hot... Is it the sun or is it just me?";
        if (collision.tag == "Player")
        {
            sceneManager.UnlockAttack();
            spriteRenderer.enabled = false;
            StartCoroutine(displayMessage());
        }
    }
}
