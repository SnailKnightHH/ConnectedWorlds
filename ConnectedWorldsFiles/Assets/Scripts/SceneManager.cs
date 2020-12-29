using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Parameters
    [SerializeField] private Transform defaltPlayerSpawnLocation;

    // References
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    private PlayerController playerController;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private UIManager uiManager;


    // Player State
    private Transform playerSpawnLocation;
    // Skill Tree
    public bool canJumpHigh = false;
    public bool canAttack = false;
    public bool canWallJump = false;
    public bool canGlide = false;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerSpawnLocation == null) playerSpawnLocation = defaltPlayerSpawnLocation;
        //playerSpawnLocation = playerController.transform;
    }

    private void Update()
    {
        uiManager.numOfHearts = playerController.maxHealth;
        uiManager.remainingHearts = playerController.remainingHealth;
    }
    public void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, playerSpawnLocation.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        playerController = player.GetComponent<PlayerController>();
        cameraFollow.target = player.transform;
    }

    public void SetSpawnPoint(Transform spawnPointLocation)
    {
        playerSpawnLocation = spawnPointLocation;
        Debug.Log("Spawn Point Set");
    }

    public void UpdateStatus()
    {
        playerController.canJumpHigh = canJumpHigh;
        playerController.UnlockJumpHigher();
        playerController.canAttack = canAttack;
        playerController.canWallJump = canWallJump;
        playerController.canGlide = canGlide;
    }
}
