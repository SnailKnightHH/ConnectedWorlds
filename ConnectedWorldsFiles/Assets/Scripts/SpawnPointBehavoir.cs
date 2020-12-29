using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehavoir : MonoBehaviour
{
    private SceneManager sceneManager;

    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        sceneManager.SetSpawnPoint(transform);
    }
}
