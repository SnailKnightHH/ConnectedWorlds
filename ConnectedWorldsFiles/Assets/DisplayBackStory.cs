using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBackStory : MonoBehaviour
{
    public static bool isGamePause = false;
    public GameObject backStoryUI;

    // Start is called before the first frame update
    void Start()
    {
        backStoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Pause()
    {
        backStoryUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;
    }

    public void Resume()
    {
        backStoryUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePause = false;
    }
}
