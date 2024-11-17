using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quit : MonoBehaviour
{
    public void QuitApplication()
    {
        // Quit the application
        Application.Quit();

        // If in the Unity Editor, stop playing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}