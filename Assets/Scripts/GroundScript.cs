using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Ground : MonoBehaviour
{
    BallLauncher ballLauncher;
    private int hitCount = 0;
    private Vector3 firstHit;

    private void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Ball hitObject = collision.gameObject.GetComponent<Ball>();
        if (hitObject == null) return; // Exit if the object is not a Ball

        Rigidbody2D hitRB = hitObject.GetComponent<Rigidbody2D>();
        if (hitRB == null) return; // Ensure Rigidbody2D exists

        if (hitObject.name.Contains("Block"))
        {
            GameManager.Instance.gameover();
        }

        if (hitObject.name.Contains("Ball"))
        {
            hitRB.velocity = Vector3.zero;
            hitRB.angularVelocity = 0f;
            hitObject.switchflying();
            hitCount++;
        }

        if(hitCount == 1)
        {
            firstHit =  hitObject.transform.position;
        }
        
        if (hitCount == ballLauncher.getBallCount())
        {
            //end turn
            GameManager.Instance.endTurn(firstHit);
        }
    }

    public void ResetHitCount()
    {
        hitCount = 0;
    }
}
