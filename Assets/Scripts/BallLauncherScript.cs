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

    public void PrepTurn(Vector3 launcherPos)
    {
        transform.position = launcherPos;

        for (int i = 0; i < collectedItems; i++)
        {
            createBall();
        }
        collectedItems = 0;


        Vector3 newPos = new Vector3(launcherPos.x, -4f, launcherPos.z);

        //shift all balls to the launcher
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

        if (allowDrag && Time.timeScale!=0)
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


        if (-direction.y > 0) // only shoot upwards
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
    }

    public void EnableDrag()
    {
        allowDrag = true;
    }

    public void DisableDrag()
    {
        allowDrag = false;
    }

    public bool AllowDrag() => allowDrag;

    public bool IsShooting() => isShooting;

    public int BallCount() => ballCount;

    public void ItemIncrement()
    {
        collectedItems++;
    }

    public void scatterBalls()
    {
        // Check if the cooldown or drag is active
        if (isCooldownActive || allowDrag) return;

        isCooldownActive = true; // Set cooldown to active

        // Create a temporary list of flying balls
        List<Ball> flyingBalls = new List<Ball>();

        // Add flying balls to the temporary list
        foreach (var ball in balls)
        {
            if (ball.getFlying())
            {
                flyingBalls.Add(ball);
            }
        }

        // Scatter all the flying balls
        foreach (var ball in flyingBalls)
        {
            ball.scatter();
        }

        // Start the cooldown coroutine
        StartCoroutine(ResetCooldownAfterDelay());
    }


    private IEnumerator ResetCooldownAfterDelay()
    {
        yield return new WaitForSeconds(scatterCooldown);
        isCooldownActive = false; // Reset cooldown
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(transform.position, Vector3.one*0.3f);
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }
}

