using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    [SerializeField] private float ghostDelay;
    [SerializeField] private float ghostDestoryDelay;
    [SerializeField] private GameObject ghost;

    private PlayerController playerController;
    Sprite currentSprite;
    [SerializeField]  private float ghostDelayTimer;
    private bool makeGhost = false;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelayTimer = ghostDelay;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        makeGhost = playerController.isDashing;
        if (makeGhost)
        {
            if (ghostDelayTimer > 0)
            {
                ghostDelayTimer -= Time.deltaTime;
            }
            else
            {
                GenerateGhost();
                ghostDelayTimer = ghostDelay;
            }
        }
    }

    private void GenerateGhost()
    {
        Debug.Log("instantiating");
        GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
        currentSprite = GetComponent<SpriteRenderer>().sprite;
        currentGhost.transform.localScale = transform.localScale;
        currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
        Destroy(currentGhost, ghostDestoryDelay);
    }
}
