using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallsUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void UpdateBallsUI(int balls)
    {
        text.SetText("Balls: " + balls.ToString());
    }
}
