using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private BallLauncher ballLauncher;
    private Ground ground;
    private BlockSpawner blockSpawner;
    private ScoreUI scoreUI;
    private BallsUI ballsUI;

    private int score;
    [SerializeField]
    private TextMeshProUGUI highscore;
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
        highscore.SetText(PlayerPrefs.GetInt("HighScore", 0).ToString());
        startGame();    
    }




    public void startGame()
    {
        ballsUI.UpdateBallsUI(ballLauncher.BallCount());
        blockSpawner.SpawnBlocks();
        ballLauncher.EnableDrag();

        time = Time.time;
    }

    public void shooting()
    {
        ballLauncher.DisableDrag();
    }

    public void endTurn(Vector3 firstHit)
    {

        TimeController tc = FindAnyObjectByType<TimeController>();
        tc.Time1();


        ballLauncher.PrepTurn(firstHit);
        ground.ResetHitCount();


        score++;
        scoreUI.UpdateScoreUI(score);
        startGame();
    }

    public void gameover()
    {
        PlayerPrefs.SetInt("Score", score);
        if(score>PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        SceneManager.LoadScene(2);
    }

    public int GetScore => score;
    public float GetTime => time;
}
