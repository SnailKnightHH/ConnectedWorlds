using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private PlatformEffector2D effector;
    [SerializeField] private float waitTimeInitial;
    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = waitTimeInitial;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(platformControl());
    }

    private IEnumerator platformControl()
    {
        if (Input.GetKey(KeyCode.S) && (player.transform.position.y > transform.position.y))
        {
            if (waitTime < 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = waitTimeInitial;
                yield return new WaitForSeconds(0.5f);
                effector.rotationalOffset = 0f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

    }

/*    private void platformReset()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))
            effector.rotationalOffset = 0;
    }*/
}
