using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    private StateMachine stateMachine;
    private BallLauncher ballLauncher;

    public StartState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.ballLauncher = GameObject.FindObjectOfType<BallLauncher>();
    }

    public void OnEnter()
    {
        Debug.Log("Entering Start State");
        ballLauncher.EnableInput(); // Enable launcher controls
    }

    public void OnUpdate()
    {
        // Listen for the launch action (e.g., mouse click) to begin shooting
        if (ballLauncher.IsLaunched)
        {
            stateMachine.ChangeState(new ShootingState(stateMachine));
        }
    }

    public void OnExit()
    {
        ballLauncher.DisableInput(); // Disable launcher controls
    }
}
