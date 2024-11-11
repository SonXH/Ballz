using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Start,
    Shooting,
    EndTurn
}


public class StateMachine : MonoBehaviour
{
    private IState currentState;

    // Start method to initialize the first state
    private void Start()
    {
        ChangeState(new StartState(this));
    }

    private void Update()
    {
        // Call the Update method of the current state each frame
        currentState?.OnUpdate();
    }

    // Method to change the current state
    public void ChangeState(IState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState?.OnEnter();
    }
}
