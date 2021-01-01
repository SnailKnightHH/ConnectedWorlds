using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public bool isGamePause = false;
    public GameObject Panel;
    //public static bool isPausedAlready = false;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }

    public void Pause()
    {
        Panel.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;
    }

    public void Resume()
    {
        Panel.SetActive(false);
        Time.timeScale = 1f;
        isGamePause = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
