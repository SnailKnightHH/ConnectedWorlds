using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public bool isGamePause = false;
    public GameObject[] panels;
    [HideInInspector]
    public WalkOnGrassSFX walkOnGrassSFX;

    private void Start()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        walkOnGrassSFX = FindObjectOfType<WalkOnGrassSFX>();
    }

    public void Pause()
    {
        walkOnGrassSFX.canPlay = false;
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true);
        }
        Time.timeScale = 0f;
        isGamePause = true;
    }

    public void Resume()
    {
        walkOnGrassSFX.canPlay = true;
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        Time.timeScale = 1f;
        isGamePause = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
