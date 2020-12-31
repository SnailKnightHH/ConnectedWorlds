using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : InGameMenu
{
    // Start is called before the first frame update
    void Start()
    {
        isGamePause = false;
    }

    // Update is called once per frame
/*    void Update()
    {
        Pausemenu();
    }

    private void Pausemenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause) Resume();
            else Pause();
        }
    }*/
}
