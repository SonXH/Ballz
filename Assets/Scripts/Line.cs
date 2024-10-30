using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    
    public LineRenderer LineRenderer;
    private Vector3 startpoint;

    // Start is called before the first frame update
    void Awake()
    {
        LineRenderer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void SetStart(Vector3 vector)
    {
        startpoint = vector;
        LineRenderer.SetPosition(0, startpoint);
    }

    public void SetEnd(Vector3 vector)
    {
        Vector3 direction = vector - startpoint;

        Vector3 endPoint = transform.position + direction;

        LineRenderer.SetPosition(1, endPoint);
    }

}
