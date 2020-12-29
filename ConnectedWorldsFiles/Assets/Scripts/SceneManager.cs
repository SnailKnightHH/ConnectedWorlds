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

    // Constants
    private const int UIMaxHealth = 10;
    private const int UIMaxMana = 10;
    // Player State
    private Transform playerSpawnLocation;
    [Range(0, 10)] [SerializeField] private int PlayerMaxHealth = 2;
    [Range(0, 10)] [SerializeField] private int PlayerMaxMana = 2;
    // Skill Tree
    public bool canJumpHigh = false;
    public bool canAttack = false;
    public bool canWallJump = false;
    public bool canGlide = false;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null) SpawnPlayer();
        else UpdateStatus();
    }

    private void Update()
    {
        uiManager.numOfHearts = playerController.maxHealth;
        uiManager.remainingHearts = playerController.remainingHealth;
        uiManager.numOfManas = playerController.maxMana;
        uiManager.remainingMana = playerController.remainingMana;
    }
    public void SpawnPlayer()
    {
        if (playerSpawnLocation == null) playerSpawnLocation = defaltPlayerSpawnLocation;
        player = Instantiate(playerPrefab, playerSpawnLocation.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        playerController = player.GetComponent<PlayerController>();
        UpdateStatus();
        cameraFollow.target = player.transform;
    }

    public void UpdateStatus()
    {
        // Update ability
        playerController.canJumpHigh = canJumpHigh;
        playerController.UnlockJumpHigher();
        playerController.canAttack = canAttack;
        playerController.canWallJump = canWallJump;
        playerController.canGlide = canGlide;

        // Update health and mana
        playerController.maxHealth = PlayerMaxHealth;
        playerController.remainingHealth = PlayerMaxHealth;
        playerController.maxMana = PlayerMaxMana;
        playerController.remainingMana = PlayerMaxMana;
    }


    // Setters:
    public void SetSpawnPoint(Transform spawnPointLocation)
    {
        playerSpawnLocation = spawnPointLocation;
        Debug.Log("Spawn Point Set");
    }

    public void IncreaseMaxHealth()
    {
        if (PlayerMaxHealth < UIMaxHealth)
        {
            PlayerMaxHealth++;
            UpdateStatus();
        }
    }
    public void IncreaseMaxMana()
    {
        if (PlayerMaxMana < UIMaxMana)
        {
            PlayerMaxMana++;
            UpdateStatus();
        }
    }

}
