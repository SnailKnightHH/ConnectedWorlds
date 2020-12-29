using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovePlatform : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform[] path;
    private int spotNumber;

    // Start is called before the first frame update
    void Start()
    {
        spotNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        movePath();
    }

    public void movePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[spotNumber].position, movementSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, path[spotNumber].position) < 0.2f)
            spotNumber++;
        if (spotNumber == path.Length) spotNumber = 0;
    }

}
