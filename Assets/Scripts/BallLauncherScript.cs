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

    private int ballCount = 3;
    private List<Ball> balls = new List<Ball>();
    private float delay = 0.1f;

    void Awake()
    {
        line = GetComponent<Line>();
        for (int i = 0; i < ballCount; i++)
        {
            createBall();
        }
    }

    private void PrepTurn()
    {
        createBall();

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

    public int getBallCount() => ballCount;
    
    
    public void createBall()
    {
        var ball = Instantiate(ballPrefab,transform.position,Quaternion.identity);
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
        StartCoroutine(LaunchBall());
    }

    private IEnumerator LaunchBall()
    {
        Vector3 direction = endPoint - startPoint;
        direction.Normalize();


        foreach (var ball in balls)
        {
            ball.GetComponent<Rigidbody2D>().AddForce(-direction * 750);
            yield return new WaitForSeconds(delay);
        }
    }

    
}
