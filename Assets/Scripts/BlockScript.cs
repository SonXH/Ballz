using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockScript : MonoBehaviour
{
    private int blockLives = 10;
    private TextMeshPro text;


    // Start is called before the first frame update
    void Awake ()
    {
        text = GetComponentInChildren<TextMeshPro>();

    }

    // Update is called once per frame
    private void UpdateVisual()
    {
        text.SetText(blockLives.ToString());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("kaput");

        blockLives--;
        if (blockLives > 0)
        {
            UpdateVisual();
        } else
        {
            Destroy(gameObject);
        }
        //ShiftBlock();
    }


    private void ShiftBlock()
    {
        transform.position -= new Vector3(0, 0.7f, 0);
    }
}
