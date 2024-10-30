using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallLauncher : MonoBehaviour
{

    private Vector3 startPoint;
    private Vector3 endPoint;

    [SerializeField]
    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

        if (Input.GetMouseButtonDown(0))
        {
            DragStarted(worldPosition);

        } else if (Input.GetMouseButton(0))
        {
            Dragging(worldPosition);
        } else if (Input.GetMouseButtonUp(0))
        {
            DragStopped();
        }
    }

    void DragStarted(Vector3 vector)
    {
        startPoint = vector;
    }

    void Dragging(Vector3 vector)
    {
        endPoint = vector;
    }

    void DragStopped()
    {
        Vector3 direction = endPoint - startPoint;

        var shoot = Instantiate(ball, transform.position, Quaternion.identity);
        shoot.GetComponent<Rigidbody2D>().AddForce(-direction);
    }

}
