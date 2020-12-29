using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public PlayerController playerController;
    // Player State
    private Transform playerSpawnLocation;
    // Skill Tree
    public bool canJumpHigh = false;
    public bool canAttack = false;
    public bool canWallJump = false;
    public bool canGlide = false;


    public void UpdateStatus()
    {
        playerController.canJumpHigh = canJumpHigh;
        playerController.UnlockJumpHigher();
        playerController.canAttack = canAttack;
        playerController.canWallJump = canWallJump;
        playerController.canGlide = canGlide;
    }
}
