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

    private Line line;

    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<Line>();
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

    void DragStarted(Vector3 startVector)
    {
        startPoint = startVector;

        line.SetStart(transform.position);
    }

    void Dragging(Vector3 endVector)
    {
        endPoint = endVector;

        Vector3 direction = endPoint - startPoint;
        line.SetEnd(transform.position - direction);

    }

    void DragStopped()
    {
        Vector3 direction = endPoint - startPoint;
        direction.Normalize();

        var shoot = Instantiate(ball, transform.position, Quaternion.identity);
        shoot.GetComponent<Rigidbody2D>().AddForce(-direction * 750);
    }

}
