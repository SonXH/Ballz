using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private BallLauncher ballLauncher;
    private Ground ground;
    private BlockSpawner blockSpawner;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        ballLauncher = FindFirstObjectByType<BallLauncher>();
        ground = FindObjectOfType<Ground>();
        blockSpawner = FindObjectOfType<BlockSpawner>();

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

    public void startGame()
    {
        blockSpawner.SpawnBlocks();
        ballLauncher.EnableDrag();
        
        Debug.Log("game started: " + ballLauncher.getBallCount() + "to shoot");
        //can drag, ball launcher in use
        //state to shooting
    }

    public void shooting()
    {

        ballLauncher.DisableDrag();


        Debug.Log("shooting balls");
        //balls shooting, no drag 
        //state to start until loss
    }

    public void endTurn(Vector3 firstHit)
    {
        Debug.Log("ending turn");


        ballLauncher.PrepTurn(firstHit);
        ground.ResetHitCount();

        startGame();

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
