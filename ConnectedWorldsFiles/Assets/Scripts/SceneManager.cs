using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Parameters
    [SerializeField] private Transform defaltPlayerSpawnLocation;

    // References
    [SerializeField] private GameObject playerPrefab;
    public GameObject player;
    private PlayerController playerController;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] public UIManager uiManager;

    // Constants
    private const int UIMaxHealth = 10;
    private const int UIMaxMana = 10;
    // Player State
    private Transform playerSpawnLocation;
    [Range(0, 10)] [SerializeField] private int PlayerMaxHealth = 2;
    [Range(0, 10)] [SerializeField] private int PlayerMaxMana = 2;
    // Skill Tree
    public bool canJumpHigh;
    [Range(0, 10)] public float normalJumpDuration;
    [Range(0, 10)] public float highJumpDuration;
    public bool canAttack;
    public bool canWallJump;
    public bool canGlide;
    public bool canDash;

    protected virtual void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null) SpawnPlayer();
        else UpdateStatus();
        player = playerController.gameObject;
    }

    private void Update()
    {
        uiManager.numOfHearts = playerController.maxHealth;
        uiManager.remainingHearts = playerController.remainingHealth;
        if (player.GetComponent<PlayerController>().canAttack)
        {
            uiManager.numOfManas = playerController.maxMana;
            uiManager.remainingMana = playerController.remainingMana;
        }

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
        if (canJumpHigh) playerController.jumpTime = highJumpDuration;
        else playerController.jumpTime = normalJumpDuration;
        playerController.canAttack = canAttack;
        playerController.canWallJump = canWallJump;
        playerController.canGlide = canGlide;
        playerController.canDash = canDash;

        // Update health and mana
        playerController.maxHealth = PlayerMaxHealth;
        playerController.remainingHealth = PlayerMaxHealth;
        if (player.GetComponent<PlayerController>().canAttack)
        {
            playerController.maxMana = PlayerMaxMana;
            playerController.remainingMana = PlayerMaxMana;
        }
    }


    // Setters:
    public void UnlockHighJump()
    {
        canJumpHigh = true;
        UpdateStatus();
    }
    
    public void UnlockAttack()
    {
        canAttack = true;
        UpdateStatus();
    }
    public void UnlockWallJump()
    {
        canWallJump = true;
        UpdateStatus();
    }
    public void UnlockGlide()
    {
        canGlide = true;
        UpdateStatus();
    }
    public void UnlockDash()
    {
        canDash = true;
        UpdateStatus();
    }

    public void SetSpawnPoint(Transform spawnPointLocation)
    {
        playerSpawnLocation = spawnPointLocation;
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
