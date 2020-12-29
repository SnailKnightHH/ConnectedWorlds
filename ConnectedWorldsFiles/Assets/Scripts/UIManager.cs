using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   // Health Display
    [SerializeField] public int remainingHearts;
    [SerializeField] public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (remainingHearts > numOfHearts) remainingHearts = numOfHearts;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < remainingHearts) hearts[i].sprite = fullHeart;
            else hearts[i].sprite = emptyHeart;

            if (i < numOfHearts) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }
    }
}
