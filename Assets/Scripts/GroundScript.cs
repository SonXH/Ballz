using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundScript : MonoBehaviour
{
    BallLauncher ballLauncher;
    private int hitCount;
    private Vector3 firstHit;

    private void Awake()
    {
        ballLauncher = GetComponent<BallLauncher>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject hitObject = collision.gameObject;
        Rigidbody2D hitRB = hitObject.GetComponent<Rigidbody2D>();

        if(hitObject.name.Contains("Ball"))
        {
            hitRB.velocity = new Vector3(0,0,0);
            hitCount++;
            //GameManager.Instance.gameover();
        }

        if(hitCount == 1)
        {
            firstHit =  hitObject.transform.position;
        }

        if(hitCount == ballLauncher.getBallCount())
        {
            //end turn
            GameManager.Instance.endTurn();
        }
    }

    public void ResethitCount()
    {
        hitCount = 0;
    }
}
