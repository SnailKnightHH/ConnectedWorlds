using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneManager sceneManager;
    private PlayerController playerController;
    public bool isInMainMenu;


    private void Start()
    {
        isInMainMenu = true;
        sceneManager = FindObjectOfType<SceneManager>();
        playerController = sceneManager.player.GetComponent<PlayerController>();
        playerController.canMove = false;
        sceneManager.uiManager.gameObject.SetActive(false);
    }


    public void PlayGame()
    {
        isInMainMenu = false;
        playerController.canMove = true;
        sceneManager.uiManager.gameObject.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
