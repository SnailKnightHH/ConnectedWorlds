using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int damage;
    public float areaOfEffect;
    [SerializeField] private GameObject explosion;
    private string destrutable = "destructableEnvironment";
    private string whatIsGround = "WalkableSurface";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemySlime slime = collision.GetComponent<EnemySlime>();
        Destructable destructableObjects = collision.GetComponent<Destructable>();
        EnemyHound hound = collision.GetComponent<EnemyHound>();

        if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3( 0f, 0f ,0f)));
            DestoryProjectile();
        }

        if (slime != null)
        {
            slime.EnemyReceiveDamage(damage);
            Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            DestoryProjectile();
        }

        if(destructableObjects != null)
        {
            destructableObjects.ReceiveDamage(damage);
            Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            DestoryProjectile();
        }

        if(hound != null)
        {
            hound.EnemyReceiveDamage(damage);
            Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            DestoryProjectile();
        }

/*        {
*//*            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                objectsToDamage[i].GetComponent<EnemySlime>().EnemyReceiveDamage(damage);
            }*//*
            
            Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            Destroy(gameObject);
        }*/

    }

    private void DestoryProjectile() {
        Debug.Log("shake");
        FindObjectOfType<CameraShake>().ShakeCamera();
        Destroy(gameObject);
    }
/*    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }*/
}
