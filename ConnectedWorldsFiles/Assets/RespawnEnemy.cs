using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    private float respawnTime = 2f;

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.transform)
        {
            
            if (child.GetComponent<EnemyClass>() != null && child.GetComponent<EnemyClass>().isDead)
            {
                StartCoroutine(EnemyRespawn(child.gameObject)); 
                child.GetComponent<EnemyClass>().isDead = false; 
            }

        }
    }

    private IEnumerator EnemyRespawn(GameObject child)
    {
        child.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        child.SetActive(true);
        child.GetComponent<EnemyClass>().RespawnHealth();
    }
}
