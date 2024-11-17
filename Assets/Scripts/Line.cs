using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer LineRenderer;
    private Vector3 startpoint;
    private bool startSet = false;
    private bool endSet = false;
    private float maxLineLength = 8f;

    // Start is called before the first frame update
    void Awake()
    {
        //LineRenderer.material.color = Color.black;

        //// Hide the line initially
        //LineRenderer.enabled = false;

        LineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Ensure a default shader
        LineRenderer.material.color = Color.black; // Set initial color
        LineRenderer.startColor = Color.black;
        LineRenderer.endColor = Color.black;
        LineRenderer.enabled = false; // Hide the line initially
    }

    // Method to set the start point of the line
    public void SetStart(Vector3 vector)
    {
        startpoint = vector;
        LineRenderer.SetPosition(0, startpoint);
        startSet = true;

        // Check if both points are set, and show the line
        CheckAndShowLine();
    }

    // Method to set the end point of the line
    public void SetEnd(Vector3 vector)
    {
        Vector3 direction = vector - startpoint;

        if (direction.magnitude > maxLineLength)
        {
            direction = direction.normalized * maxLineLength;
        }


        Vector3 endPoint = transform.position + direction;

        LineRenderer.SetPosition(1, endPoint);
        endSet = true;

        // Check if both points are set, and show the line
        CheckAndShowLine();
    }

    // Method to hide the line
    public void HideLine()
    {
        LineRenderer.enabled = false;
        startSet = false;
        endSet = false;
    }

    // Checks if both start and end points are set and enables the line renderer if true
    private void CheckAndShowLine()
    {
        if (startSet && endSet)
        {
            LineRenderer.enabled = true;
        }
    }
}

