using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private enum GameState
    {
        Menu,
        Start,
        Shot,
        Pause,
        Loss 
    }
    public void switchStates(int aas)
    {
    }
    public void startGame()
    {
        Debug.Log("game started");
        //can drag, ball launcher in use
        //state to shooting
    }

    public void shooting()
    {
        Debug.Log("shooting balls");
        //balls shooting, no drag 
        //state to start until loss
    }

    public void endTurn()
    {
        Debug.Log("ending turn");
        // blocks shifted down, blocks spawning 
        //display switch to game over screen
    }

    public void gameover()
    {
        Debug.Log("maybe next time you are also in the winning team");
        //display mean message
        //options for restart
    }

    public void pauseGame()
    {
        Debug.Log("pee pause");
        //display pause screen
        //options resume and restart
    }
}
