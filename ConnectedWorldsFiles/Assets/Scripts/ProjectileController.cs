using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Parameter
    public int damage;

    // References
    [SerializeField] private GameObject explosion;
    private string whatIsGround = "WalkableSurface";
    private string whatIsEnemies = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask collidedLayer = collision.gameObject.layer;

        if (collidedLayer == LayerMask.NameToLayer(whatIsGround))
        {
            DestoryProjectile();
        }
        else if (collidedLayer == LayerMask.NameToLayer(whatIsEnemies))
        {
            collision.GetComponent<EnemyClass>().ReceiveDamage(damage);
            DestoryProjectile();
        }
    }

    private void DestoryProjectile() {
        FindObjectOfType<CameraShake>().ShakeCamera();
        Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        Destroy(gameObject);
    }
}
