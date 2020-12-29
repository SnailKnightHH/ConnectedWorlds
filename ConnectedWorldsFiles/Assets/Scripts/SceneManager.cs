using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public PlayerController playerController;
    // Skill Tree
    public bool canJumpHigh = false;
    public bool canAttack = false;
    public bool canWallJump = false;
    public bool canGlide = false;


    public void ChangeStatus()
    {
        if (canJumpHigh) { playerController.canJumpHigh = true; playerController.UnlockJumpHigher(); }
        if (canAttack) playerController.canAttack = true;
        if (canWallJump) playerController.canWallJump = true;
        if (canGlide) playerController.canGlide = true;
    }
}
