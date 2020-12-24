using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
       // GameObject player = GetComponent<PlayerMovement>().gameObject;
        if (player.CompareTag("Player"))
        {
            Destroy(player);
        }
    }

    
    
}
