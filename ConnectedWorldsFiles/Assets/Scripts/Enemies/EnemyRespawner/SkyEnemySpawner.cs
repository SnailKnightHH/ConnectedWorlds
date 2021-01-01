using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkyEnemySpawner : MonoBehaviour
{

    [SerializeField] private float respawnTime = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.transform)
        {
            if (child.GetComponentInChildren<SkyEnemyClass>() != null && child.GetComponentInChildren<SkyEnemyClass>().isDead)
            {
                StartCoroutine(EnemyRespawn(child.gameObject));
                child.GetComponentInChildren<SkyEnemyClass>().isDead = false;
            }

        }

    }

    private IEnumerator EnemyRespawn(GameObject child)
    {
        child.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        child.SetActive(true);
        child.GetComponentInChildren<SkyEnemyClass>().spriteRenderer.color = Color.white;
        child.GetComponentInChildren<SkyEnemyClass>().RespawnHealth();
    }


}
