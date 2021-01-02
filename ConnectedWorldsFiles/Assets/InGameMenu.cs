using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public bool isGamePause = false;
    public GameObject[] panels;

    private void Start()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void Pause()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true);
        }
        Time.timeScale = 0f;
        isGamePause = true;
        Debug.Log("pause");
    }

    public void Resume()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        Time.timeScale = 1f;
        isGamePause = false;
        Debug.Log("resume");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
