﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockJumpHigher : SkillTreeClass
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        theMessage = "Imagine being able to jump 10-feet high...";
        if (collision.tag == "Player")
        {
            player.canJumpHigh = true;
            spriteRenderer.enabled = false;
            StartCoroutine(displayMessage());
        }
    }
}
