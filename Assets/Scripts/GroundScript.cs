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


            Ball ballScript = hitObject.GetComponent<Ball>();

            // If the ballScript component is found, call the ballLanded function
            //if (ballScript != null)
            //{
            //    ballScript.ballLanded();  // Call the ballLanded method
            //}
            //else
            //{
            //    Debug.LogWarning("BallScript not found on the object.");

            //}

            //GameManager.Instance.gameover();
        }

    }
}
