using System.Collections;
using System.Collections.Generic;
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

        GameObject hitObject = collision.gameObject;
        Rigidbody2D hitRB = hitObject.GetComponent<Rigidbody2D>();

        if(hitObject.name.Contains("Ball"))
        {
            hitRB.velocity = new Vector3(0,0,0);
            //hitObject.SetActive(false);
            hitCount++;
            //GameManager.Instance.gameover();
        }

        if(hitCount == 1)
        {
            firstHit =  hitObject.transform.position;
            Debug.Log("first hit: " + firstHit);
        }
        Debug.Log(hitCount + "ball(s) hit the ground out of" + ballLauncher.getBallCount());
        if (hitCount == ballLauncher.getBallCount())
        {
            Debug.Log("all hit ground");
            //end turn
            GameManager.Instance.endTurn(firstHit);
        }
    }

    public void ResetHitCount()
    {
        hitCount = 0;
    }
}
