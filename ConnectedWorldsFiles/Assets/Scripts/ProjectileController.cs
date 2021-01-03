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
    private string whatIsSkyEnemies = "SkyEnemy";
    private string whatIsDestructableEnvironment = "destructableEnvironment";
    private string whatIsBlockers = "Blockers";

    private AudioSource audioSource;
    private CircleCollider2D circleCollider2D;
    [SerializeField] private GameObject ParticleSystemGameObject;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask collidedLayer = collision.gameObject.layer;

        if (collidedLayer == LayerMask.NameToLayer(whatIsGround))
        {
            StartCoroutine(DestoryProjectile());
        }
        else if (collidedLayer == LayerMask.NameToLayer(whatIsEnemies))
        {
            collision.GetComponent<EnemyClass>().ReceiveDamage(damage);
            StartCoroutine(DestoryProjectile());
        }
        else if (collidedLayer == LayerMask.NameToLayer(whatIsSkyEnemies))
        {
            collision.GetComponent<SkyEnemyClass>().ReceiveDamage(damage);
            StartCoroutine(DestoryProjectile());
        }
        else if(collidedLayer == LayerMask.NameToLayer(whatIsDestructableEnvironment))
        {
            collision.GetComponent<Destructable>().ReceiveDamage(damage);
            StartCoroutine(DestoryProjectile());
        }
        else if(collidedLayer == LayerMask.NameToLayer(whatIsBlockers))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestoryProjectile() {
        ParticleSystemGameObject.SetActive(false);
        circleCollider2D.enabled = false;
        FindObjectOfType<CameraShake>().ShakeCamera();
        Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
