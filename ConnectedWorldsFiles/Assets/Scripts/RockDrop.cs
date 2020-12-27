using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDrop : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Rigidbody2D rockRB; // set to kinematics
    [SerializeField] private float dropSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            rockRB.velocity = new Vector2(0, -1) * dropSpeed;
    }
}
