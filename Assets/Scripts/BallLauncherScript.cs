using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private bool canReceiveInput = false;

    private Vector3 startPoint;
    private Vector3 endPoint;

    [SerializeField]
    Ball ball;

    public int ballCount = 10;

    private Line line;

    private bool isLaunched = false;
    private List<Ball> balls = new List<Ball>();
    private Vector3 firstBallLandedPosition;
    private bool allBallsLanded = false;

    private float delay = 0.1f;  // Delay in seconds


    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<Line>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canReceiveInput && !isLaunched)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

            if (Input.GetMouseButtonDown(0))
            {
                DragStarted(worldPosition);

            }
            else if (Input.GetMouseButton(0))
            {
                Dragging(worldPosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                DragStopped();
            }
        }

        if (isLaunched && !allBallsLanded)
        {
            CheckAllBallsLanded();
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

    // Change to Coroutine
    void DragStopped()
    {

        // Start the Coroutine to launch the balls with delay
        StartCoroutine(LaunchBallsWithDelay());
    }

    // Coroutine to launch balls with delay
    IEnumerator LaunchBallsWithDelay()
    {
        Vector3 direction = endPoint - startPoint;
        direction.Normalize();

        foreach (var ball in balls)
        {
            //var ballShot = Instantiate(ball, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction * 750);
            //balls.Add(ballShot);

            // Wait for the delay before launching the next ball
            yield return new WaitForSeconds(delay);
        }
        //ballCount = 0;
    }

    public void EnableInput()
    {
        canReceiveInput = true;
    }

    public void DisableInput()
    {
        canReceiveInput = false;
    }

    public bool IsLaunched => isLaunched;


    private void CheckAllBallsLanded()
    {
        allBallsLanded = true;

        foreach (Ball ball in balls)
        {
            if (ball != null && ball.transform.position.y > 0.1f)
            {
                allBallsLanded = false;
                break;
            }
        }

        if (allBallsLanded)
        {
            firstBallLandedPosition = balls[0].transform.position;
        }
    }

    public bool AllBallsLanded()
    {
        return allBallsLanded;
    }

    public Vector3 GetFirstLandedBallPosition()
    {
        return firstBallLandedPosition;
    }

    public void ClearBalls()
    {
        foreach (Ball ball in balls)
        {
            if (ball != null)
            {
                Destroy(ball); // Destroy the ball GameObject in the scene
            }
        }
        balls.Clear(); // Clear the list to remove all references
        allBallsLanded = false; // Reset the flag for the next turn
        isLaunched = false; // Reset the launch flag for the next turn
    }
}
