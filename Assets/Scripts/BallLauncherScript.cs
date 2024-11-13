using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallLauncher : MonoBehaviour
{

    private Vector3 startPoint;
    private Vector3 endPoint;

    [SerializeField]
    Ball ballPrefab;

    private Line line;

    private int ballCount = 0;
    private List<Ball> balls = new List<Ball>();
    private float delay = 0.1f;

    private bool allowDrag;

    private bool isShooting;

    void Awake()
    {
        allowDrag = false;
        line = GetComponent<Line>();
        createBall();
        isShooting = false;
    }

    private void Start()
    {
        //for (int i = 0; i < ballCount; i++)
        //{
        //    createBall();
        //}
    }

    public void PrepTurn(Vector3 launcherPos)
    {
        //Debug.Log("prepping");
        //createBall();
        transform.position = launcherPos;

        Vector3 newPos = new Vector3(launcherPos.x, -4f,  launcherPos.z);

        foreach(var ball in balls)
        {
            ball.transform.position = launcherPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

        if (allowDrag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DragStarted(worldPosition);

            } else if (Input.GetMouseButton(0))
            {
                Dragging(worldPosition);

            } else if (Input.GetMouseButtonUp(0) && startPoint != Vector3.zero && endPoint != Vector3.zero)
            {
                DragStopped();
            }
        }
    }

    public int getBallCount() => ballCount;
    
    
    public void createBall()
    {
        var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ballCount++;
        balls.Add(ball);
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
  
        line.HideLine();
        if (!isShooting)
        {
            StartCoroutine(LaunchBall());
        }
        

    }

    private IEnumerator LaunchBall()
    {
        Vector3 direction = endPoint - startPoint;
        direction.Normalize();

        GameManager.Instance.shooting();
        if(-direction.y > 0)
        {
            isShooting = true;
            foreach (var ball in balls)
            {
                ball.GetComponent<Rigidbody2D>().AddForce(-direction * 700);
                yield return new WaitForSeconds(delay);
            }
            isShooting = false;
        }

        //Debug.Log("boop");
        //DisableDrag();

        //foreach (var ball in balls)
        //{
        //    ball.GetComponent<Rigidbody2D>().AddForce(-direction * 700);
        //    yield return new WaitForSeconds(delay);
        //}
    }

    public void EnableDrag()
    {
        allowDrag = true;
    }

    public void DisableDrag()
    {
        allowDrag = false;
    }

    public bool IsShooting() => isShooting;

}
