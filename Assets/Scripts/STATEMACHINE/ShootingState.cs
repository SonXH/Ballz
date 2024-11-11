using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : IState
{
    private StateMachine stateMachine;
    private BallLauncher ballLauncher;

    public ShootingState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.ballLauncher = GameObject.FindObjectOfType<BallLauncher>();
    }

    public void OnEnter()
    {
        Debug.Log("Entering Shooting State");
        // Start launching balls
    }

    public void OnUpdate()
    {
        // Check if all balls have landed
        if (ballLauncher.AllBallsLanded())
        {
            Vector3 firstBallPosition = ballLauncher.GetFirstLandedBallPosition();
            Debug.Log(firstBallPosition);
            stateMachine.ChangeState(new EndState(stateMachine, firstBallPosition));
        }
    }

    public void OnExit()
    {
        Debug.Log("Exiting Shooting State");
    }
}
