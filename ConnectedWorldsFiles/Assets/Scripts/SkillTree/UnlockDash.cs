using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDash : SkillTreeClass
{

    public DisplayUnlockDash displayUnlockDash;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        theMessage = "Shift or right click to dash";
        if (collision.tag == "Player")
        {
            sceneManager.UnlockDash();
            displayUnlockDash.Pause();
            Destroy(gameObject);
        }
    }
}
