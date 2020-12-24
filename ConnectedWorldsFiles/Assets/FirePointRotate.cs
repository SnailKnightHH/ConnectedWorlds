using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointRotate : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D firePointRB;
    public Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        
    }

    public void firePointAiming()
    {
        transform.position = playerRB.position;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - playerRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firePointRB.rotation = angle;
        Debug.Log(lookDir + " " + angle);
    }
}
