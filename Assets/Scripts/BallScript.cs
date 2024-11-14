using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    float rotationSpeed = 90f;

    [SerializeField]
    private float moveSpeed = 10;

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
        rigidbody2d.velocity  *= moveSpeed;
    }
    private void Update()
    {
        //Debug.Log(rigidbody2d.velocity.y);
        //Debug.Log(launcher.IsShooting());
        float absy = Mathf.Abs(rigidbody2d.velocity.y);
        
        if (absy < 0.1f && flying)
        {
            Debug.Log(rigidbody2d.velocity.y);
            
            int chance = UnityEngine.Random.Range(1, 2);

            
            float y = chance == 1 ? 0.3f : -0.3f;

            //float y = 0.1f;

            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, y);

            Debug.Log(y);
        }
        if (flying)
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }

    public void switchflying()
    {
        flying = !flying;
    }
}
