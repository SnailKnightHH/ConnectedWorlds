using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockAttack : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private TextMeshProUGUI unlockAttackMessage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float displayTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.canAttack = true;
            spriteRenderer.enabled = false;
            StartCoroutine(displayMessage());
        }

    }

    IEnumerator displayMessage()
    {
        unlockAttackMessage.SetText("Wow my hands are feeling a bit hot... Is it the sun or is it just me?");
        yield return new WaitForSeconds(displayTime);
        unlockAttackMessage.alpha = 0;
    }
}
