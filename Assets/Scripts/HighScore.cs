using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highscore;

    [SerializeField]
    private TextMeshProUGUI score;

    void Start()
    {
        highscore.SetText(PlayerPrefs.GetInt("HighScore", 0).ToString());
        score.SetText(PlayerPrefs.GetInt("Score", 0).ToString());
    }
}
