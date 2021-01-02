using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnPointBehavoir : MonoBehaviour
{
    private SceneManager sceneManager;
    private TextMeshProUGUI textMeshProUGUI;


    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.alpha = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sceneManager.SetSpawnPoint(transform);
            StartCoroutine(displayText());
        }
       
    }

    private IEnumerator displayText()
    {
        textMeshProUGUI.alpha = 1f;
        yield return new WaitForSeconds(3f);
        textMeshProUGUI.alpha = 0f;
    }
}
