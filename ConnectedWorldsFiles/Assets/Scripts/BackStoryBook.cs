using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackStoryBook : MonoBehaviour
{
    private SceneManager sceneManager;
    private GameObject player;
    public DisplayBackStory displayBackStory;
    [SerializeField] private float distance;
    [SerializeField] private GameObject interactHint;
    private Image interactHintImage;

    // Start is called before the first frame update
    void Start()
    {
        interactHintImage = interactHint.GetComponent<Image>();
        var tempColor = interactHintImage.color;
        tempColor.a = 0f;
        interactHintImage.color = tempColor;
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
            var tempColor = interactHintImage.color;
            tempColor.a = 1f;
            interactHintImage.color = tempColor;
            if (Input.GetKeyDown(KeyCode.E))
            {
                displayBackStory.Pause();
                //displayBackStory.isGamePause = true;
            }
        }
        else
        {
            var tempColor = interactHintImage.color;
            tempColor.a = 0f;
            interactHintImage.color = tempColor;
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
