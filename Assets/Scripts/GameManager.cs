using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private BallLauncher ballLauncher;
    private Ground ground;
    private BlockSpawner blockSpawner;
    private ScoreUI scoreUI;
    private BallsUI ballsUI;

    private int score;
    private float time;

    private void Awake()
    {
        score = 1;

        // If there is an instance, and it's not me, delete myself.
        ballLauncher = FindFirstObjectByType<BallLauncher>();
        ground = FindObjectOfType<Ground>();
        blockSpawner = FindObjectOfType<BlockSpawner>();

        scoreUI = FindObjectOfType<ScoreUI>();
        ballsUI = FindObjectOfType<BallsUI>();

        if (
            Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    private void Start()
    {
        startGame();    
    }




    public void startGame()
    {
        ballsUI.UpdateBallsUI(ballLauncher.BallCount());
        blockSpawner.SpawnBlocks();
        ballLauncher.EnableDrag();
        
        Debug.Log("game started: " + ballLauncher.getBallCount());
        //can drag, ball launcher in use
        //state to shooting

        time = Time.time;
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

        
        Debug.Log("turn ended");

        TimeController tc = FindAnyObjectByType<TimeController>();
        tc.Time1();


        ballLauncher.PrepTurn(firstHit);
        ground.ResetHitCount();


        score++;
        scoreUI.UpdateScoreUI(score);
        startGame();

        // blocks shifted down, blocks spawning 
        //display switch to game over screen
    }

    public void gameover()
    {
        Debug.Log("maybe next time you are also in the winning team. you loser losing like a never winning human being");

        SceneManager.LoadScene(2);
        //display mean message
        //options for restart
    }

    public void pauseGame()
    {
        Debug.Log("pee pause");
        //display pause screen
        //options resume and restart
    }

    public int GetScore => score;
    public float GetTime => time;
}
