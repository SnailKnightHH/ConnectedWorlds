using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustedRobotBullet : MonoBehaviour
{
    private GameObject player;
    private SceneManager sceneManager;
    private string whatIsGround = "WalkableSurface";
    [SerializeField] private int bulletDamage;
    [SerializeField] private GameObject explosion;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = sceneManager.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamage(bulletDamage);
            StartCoroutine(DestoryProjectile());
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            StartCoroutine(DestoryProjectile());
        }

    }
    private IEnumerator DestoryProjectile()
    {
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        audioSource.Play();
        FindObjectOfType<CameraShake>().ShakeCamera();
        Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
