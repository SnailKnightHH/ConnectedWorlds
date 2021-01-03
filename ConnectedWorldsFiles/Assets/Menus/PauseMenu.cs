using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : InGameMenu
{
    [SerializeField] private MainMenu mainMenu;

/*    private void Start()
    {
        if (!mainMenu.isInMainMenu)
            Pausemenu();
        
    }*/

    // Update is called once per frame
    void Update()
    {
        if (!mainMenu.isInMainMenu)
            Pausemenu();
    }

    private void Pausemenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach(GameObject panel in panels)
            {
                panel.SetActive(false);
            }

            if (isGamePause)
            {
                Resume();
                isGamePause = false;
            }
            else
            {
                pausing();
                isGamePause = true;
            }
        }

    }

    public void pausing()
    {
        walkOnGrassSFX.canPlay = false;
        foreach (GameObject panel in panels)
        {
            if(panel.gameObject.name == "PauseMenuPanel")
                panel.SetActive(true);
        }
        Time.timeScale = 0f;
        isGamePause = true;
    }

}
