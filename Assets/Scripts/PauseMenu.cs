using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject pauseButton;

    private bool isPaused;

    AudioManager audioManager;

    BallLauncher ballLauncher;

    void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){

            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        
        
        pauseMenu.SetActive(true);
        TimeController tc = FindAnyObjectByType<TimeController>();
        tc.stopTime();
        isPaused = true;
        pauseButton.SetActive(false);
        
        


        audioManager.PlayPauseMusic();
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        TimeController tc = FindAnyObjectByType<TimeController>();
        tc.Time1();
        isPaused = false;
        pauseButton.SetActive(true);   
        

        audioManager.ResumeBackgroundMusic();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
