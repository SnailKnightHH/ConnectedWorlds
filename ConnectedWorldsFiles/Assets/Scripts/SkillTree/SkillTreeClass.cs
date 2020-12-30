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
    [HideInInspector]
    public SceneManager sceneManager;

    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator displayMessage()
    {
        unlockAbilityMessage.SetText(theMessage);
        yield return new WaitForSeconds(displayTime);
        unlockAbilityMessage.alpha = 0;
    }
}
