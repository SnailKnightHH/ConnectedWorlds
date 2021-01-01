using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockGlide : SkillTreeClass
{

    public DisplayUnlockGlide displayUnlockGlide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        theMessage = "Lighter body means more free coffee! Could someone direct me to the nearest bar?";
        if (collision.tag == "Player")
        {
            sceneManager.UnlockGlide();
            displayUnlockGlide.Pause();
            Destroy(gameObject);
        }
    }

}
