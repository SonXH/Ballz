using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){

            Debug.Log("Escape key pressed!");
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
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        TimeController tc = FindAnyObjectByType<TimeController>();
        tc.Time1();
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
