using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackStoryBook : MonoBehaviour
{
    private SceneManager sceneManager;
    private GameObject player;
    public DisplayBackStory displayBackStory;
    [SerializeField] private float distance;
    [SerializeField] private TextMeshProUGUI interactHint;

    // Start is called before the first frame update
    void Start()
    {
        interactHint.alpha = 0f;
        sceneManager = FindObjectOfType<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        player = sceneManager.player;
        if (displayBackStory.isGamePause) quitWindow();
        else Interact();
    }

    private void Interact()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < distance)
        {
            interactHint.alpha = 1f;
            if (Input.GetKeyDown(KeyCode.E))
            {
                displayBackStory.Pause();
                displayBackStory.isGamePause = true;
            }
        }
        else
        {
            interactHint.alpha = 0f;
        }
    }

    private void quitWindow()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            displayBackStory.Resume();
            //DisplayBackStory.isGamePause = false;
        }
           
    }
}
