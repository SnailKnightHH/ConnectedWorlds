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

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
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
            DestoryProjectile();
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer(whatIsGround))
        {
            DestoryProjectile();
        }
    }
    private void DestoryProjectile()
    {
        FindObjectOfType<CameraShake>().ShakeCamera();
        Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        Destroy(gameObject);
    }
}
