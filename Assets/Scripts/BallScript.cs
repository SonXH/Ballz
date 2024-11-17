using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private float forceAmount = 750f;
    public float maxSpeed = 26f;
    //[SerializeField]
    //private float moveSpeed = 10f;

    [SerializeField]
    private Rigidbody2D rigidbody2d;

    BallLauncher launcher;

    private bool flying;


    // Start is called before the first frame update
    void Awake()
    { 
        launcher = FindAnyObjectByType<BallLauncher>();
        flying = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidbody2d.velocity  = moveSpeed;

        //rigidbody2d.velocity *= moveSpeed;
        if (rigidbody2d.velocity.magnitude > maxSpeed)
        {
            rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, maxSpeed);
        }
    }
    private void Update()
    {
        
        float absy = Mathf.Abs(rigidbody2d.velocity.y);
        
        if (absy < 0.1f && flying)
        {
            
            int chance = UnityEngine.Random.Range(1, 2);

            
            float y = chance == 1 ? 0.3f : -0.3f;

            //float y = 0.1f;

            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, y);
        }

    }
    public bool getFlying() => flying;

    public void switchflying()
    {
        flying = !flying;
        if (!flying)
        {
            // If the ball stops flying, reset its velocity to prevent undesired movement
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    public void scatter()
    {
        if (flying)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            //rigidbody2d.velocity = randomDirection * moveSpeed;
            rigidbody2d.AddForce(randomDirection * forceAmount);
        }
    }

}
