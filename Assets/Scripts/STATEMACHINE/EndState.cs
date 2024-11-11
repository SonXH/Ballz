using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndState : IState
{
    private StateMachine stateMachine;
    private Vector3 newLauncherPosition;
    private BlockSpawner blockSpawner;
    private BallLauncher ballLauncher;

    public EndState(StateMachine stateMachine, Vector3 newLauncherPosition)
    {
        this.stateMachine = stateMachine;
        this.newLauncherPosition = newLauncherPosition;
        this.blockSpawner = GameObject.FindObjectOfType<BlockSpawner>();
        this.ballLauncher = GameObject.FindObjectOfType<BallLauncher>();
    }

    public void OnEnter()
    {
        Debug.Log("Entering EndTurn State");

        // Move launcher to the new position
        ballLauncher.transform.position = newLauncherPosition;

        // Spawn new blocks for the next turn
        blockSpawner.SpawnBlocks();

        // Clear the balls from the previous turn
        ballLauncher.ClearBalls();

        // Transition back to Start state
        stateMachine.ChangeState(new StartState(stateMachine));
    }

    public void OnUpdate() { }

    public void OnExit()
    {
        Debug.Log("Exiting EndTurn State");
    }
}
