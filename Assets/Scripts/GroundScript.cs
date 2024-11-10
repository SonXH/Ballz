using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject hitObject = collision.gameObject;
        Rigidbody2D hitRB = hitObject.GetComponent<Rigidbody2D>();

        if(hitObject.name.Contains("Ball"))
        {
            hitRB.velocity = new Vector3(0,0,0);

            GameManager.Instance.gameover();
        }
    }
}
