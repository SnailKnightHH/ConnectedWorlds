using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOnGrassSFX : MonoBehaviour
{
    private SceneManager sceneManager;
    private GameObject player;
    private PlayerController playerController;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] walkOnGrassSFX;
    private int randomNum;
    //[HideInInspector]
    public bool canPlay;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        audioSource = GetComponent<AudioSource>();
        randomNum = Random.Range(0, walkOnGrassSFX.Length);
        canPlay = true;
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
        if (playerController.grounded && playerController.horizontalInput != 0 && canPlay)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(walkOnGrassSFX[randomNum]);
            randomNum = Random.Range(0, walkOnGrassSFX.Length);
        }
        else
        {
            audioSource.Stop();
        }

    }

}
