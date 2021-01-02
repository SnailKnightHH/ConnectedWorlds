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

    // Mana Display
    [SerializeField] public int remainingMana;
    [SerializeField] public int numOfManas;
    public Image[] manas;
    public Sprite fullMana;
    public Sprite emptyMana;

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        UpdateManaUI();
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

    private void UpdateManaUI()
    {
        if (remainingMana > numOfManas) remainingMana = numOfManas;
        for (int i = 0; i < manas.Length; i++)
        {
            if (i < remainingMana) manas[i].sprite = fullMana;
            else manas[i].sprite = emptyMana;

            if (i < numOfManas) manas[i].enabled = true;
            else manas[i].enabled = false;
        }
    }
}
