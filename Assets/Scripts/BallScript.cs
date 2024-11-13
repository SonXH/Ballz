using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;

    [SerializeField]
    private Rigidbody2D rigidbody2d;


    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2d.velocity  *= moveSpeed;
    }
    private void Update()
    {
        float absy = Mathf.Abs(rigidbody2d.velocity.y);
        if(absy < 0.1f)
        {
            int chance = UnityEngine.Random.Range(1, 2);

            float y = chance == 1 ? 0.1f : -0.1f;

            rigidbody2d.velocity.Set(rigidbody2d.velocity.x,y);
        }
    }
}
