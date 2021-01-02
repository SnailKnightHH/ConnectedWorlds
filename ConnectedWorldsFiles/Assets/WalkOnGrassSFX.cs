using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOnGrassSFX : MonoBehaviour
{
    private SceneManager sceneManager;
    private GameObject player;
    private PlayerController playerController;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        player = sceneManager.player;
        playerController = player.GetComponent<PlayerController>();
        PlayWalkOnGrassAudio();
    }

    private void PlayWalkOnGrassAudio()
    {
        if (playerController.grounded && playerController.horizontalInput != 0)
        {
            if(!audioSource.isPlaying)
                audioSource.Play(); 
        }
        else
        {
            audioSource.Stop();
        }

    }
}
