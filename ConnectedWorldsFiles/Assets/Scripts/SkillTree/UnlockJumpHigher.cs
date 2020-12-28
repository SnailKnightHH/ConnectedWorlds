using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockJumpHigher : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private TextMeshProUGUI unlockJumpHigherMessage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float displayTime;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.canJumpHigh = true;
            spriteRenderer.enabled = false;
            StartCoroutine(displayMessage());
        }
    }

    IEnumerator displayMessage()
    {
        unlockJumpHigherMessage.SetText("Imagine being able to jump 10-feet high...");
        yield return new WaitForSeconds(displayTime);
        unlockJumpHigherMessage.alpha = 0;
    }
}
