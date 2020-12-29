using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreeClass : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI unlockAbilityMessage;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private float displayTime;
    [HideInInspector]
    public string theMessage;
    public SceneManager sceneManager;

    public IEnumerator displayMessage()
    {
        unlockAbilityMessage.SetText(theMessage);
        yield return new WaitForSeconds(displayTime);
        unlockAbilityMessage.alpha = 0;
    }
}
