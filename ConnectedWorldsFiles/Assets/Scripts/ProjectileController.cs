using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int damage;
    public float areaOfEffect;
    [SerializeField] private GameObject explosion;
    public LayerMask destructableEnvironment; 

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Wall") || collider2D.CompareTag("Ground"))
        {
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, destructableEnvironment);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                objectsToDamage[i].GetComponent<Destructable>().ReceiveDamage(damage);
            }
            Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3( 0f, 0f ,0f))); 
            Destroy(gameObject);
        }
    }

/*    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }*/
}
