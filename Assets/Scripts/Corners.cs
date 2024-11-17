using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corners : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitObject = collision.gameObject;
        if (hitObject.name.Contains("Ball") && hitObject != null) {

            Rigidbody2D hitRB = hitObject.GetComponent<Rigidbody2D>();
            hitRB.AddForce(hitRB.velocity * 5);
        }

    }
}
