using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Rigidbody2D slimeRB;
    private int horizontalMove;

    private void Awake()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        horizontalMove = 1;
    }
    public void Changedirection()
    {
        horizontalMove = -horizontalMove;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void FixedUpdate()
    {
        slimeRB.velocity = new Vector2(horizontalMove * speed, 0f);
    }
}
