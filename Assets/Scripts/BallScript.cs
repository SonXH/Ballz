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
        float absx = Mathf.Abs(rigidbody2d.velocity.x);
        if(absx < 0.1f)
        {
            int chance = UnityEngine.Random.Range(1, 2);

            
            float x = chance == 1 ? 0.2f : -0.2f;

            //float x = 0.1f;

            rigidbody2d.velocity.Set(x, rigidbody2d.velocity.y);
        }
    }
}
