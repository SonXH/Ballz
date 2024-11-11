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
    void Update()
    {
        rigidbody2d.velocity *= moveSpeed;
    }
}
