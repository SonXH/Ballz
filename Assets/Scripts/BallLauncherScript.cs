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

    private int collectedItems = 0;


    private float scatterCooldown = 2.5f; // Cooldown duration in seconds
    private bool isCooldownActive;


    void Awake()
    {
        allowDrag = false;
        isShooting = false;
        isCooldownActive=false;
        line = GetComponent<Line>();
        createBall();
    }

    private void Start()
    {
        //for (int i = 0; i < ballCount; i++)
        //{
        //    createBall();
        //}
        Debug.Log($"Initial State - isShooting: {isShooting}, allowDrag: {allowDrag}, isCooldownActive: {isCooldownActive}");
    }


    public void PrepTurn(Vector3 launcherPos)
    {
        //Debug.Log("prepping");
        //createBall();
        transform.position = launcherPos;

        for (int i = 0; i < collectedItems; i++)
        {
            createBall();
        }
        collectedItems = 0;


        Vector3 newPos = new Vector3(launcherPos.x, -4f, launcherPos.z);

        foreach (var ball in balls)
        {
            ball.transform.position = launcherPos;
        }
        StartCoroutine(ResetCooldownAfterDelay());
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

            }
            else if (Input.GetMouseButton(0))
            {
                Dragging(worldPosition);

            }
            else if (Input.GetMouseButtonUp(0) && startPoint != Vector3.zero && endPoint != Vector3.zero)
            {
                DragStopped();
            }
        }
    }

    public int getBallCount() => ballCount;


    private void createBall()
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
        if (isShooting) yield break;

        Vector3 direction = endPoint - startPoint;
        direction.Normalize();
        isShooting = true;


        if (-direction.y > 0)
        {
            GameManager.Instance.shooting();
            foreach (var ball in balls)
            {
                ball.GetComponent<Rigidbody2D>().AddForce(-direction * 750);
                ball.switchflying();
                yield return new WaitForSeconds(delay);
            }
        }
        isShooting = false;
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
        Debug.Log("Drag Enabled");
    }

    public void DisableDrag()
    {
        allowDrag = false;
        Debug.Log("Drag Disabled");
    }

    public bool IsShooting() => isShooting;

    public int BallCount() => ballCount;

    public void ItemIncrement()
    {
        collectedItems++;
    }

    //public void scatterBalls()
    //{
    //    foreach (var ball in balls)
    //    {
    //        ball.scatter();
    //    }
    //}

    public void scatterBalls()
    {
        //if (!isCooldownActive)
        //{
        //    foreach (var ball in balls)
        //    {
        //        if (ball.getFlying())
        //        {
        //            ball.scatter();
        //        }

        //    }

        //    // Start cooldown
        //    isCooldownActive = true;
        //    StartCoroutine(ResetCooldownAfterDelay());
        //}

        if(isCooldownActive||allowDrag)return;


        isCooldownActive = true; // Set cooldown to active
        foreach (var ball in balls)
        {
            if (ball.getFlying())
            {
                ball.scatter();
            }
        }

        Debug.Log("ScatterBalls called. isShooting: " + isShooting + ", allowDrag: " + allowDrag);

        StartCoroutine(ResetCooldownAfterDelay());
    }
      
    private IEnumerator ResetCooldownAfterDelay()
    {
        Debug.Log("Cooldown active. Waiting...");
        yield return new WaitForSeconds(scatterCooldown);
        isCooldownActive = false; // Reset cooldown
        Debug.Log("Cooldown reset. Scatter can be used again.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(transform.position, Vector3.one*0.3f);
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }
}

