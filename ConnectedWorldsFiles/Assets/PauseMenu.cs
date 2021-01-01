using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : InGameMenu
{

    // Update is called once per frame
     void Update()
    {
        Pausemenu();
    }

    private void Pausemenu()
    {
       // Debug.Log(isGamePause);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause) 
            { 
                Resume();
                isGamePause = false;
                Debug.Log("received1" + isGamePause); 
            }
            else 
            { 
                Pause();
                isGamePause = true;
                Debug.Log("received2" + isGamePause); 
            }
        }
        
    }
}
