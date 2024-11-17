using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    private int blockLives;
    private TextMeshPro text;
    private SpriteRenderer spriteRenderer;



    // Start is called before the first frame update
    void Awake ()
    {
        text = GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        blockLives = GameManager.Instance.GetScore;
        UpdateVisual();
    }

    // Update is called once per frame
    private void UpdateVisual()
    {
        text.SetText(blockLives.ToString());
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, blockLives / 50f);
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
    }

    public void SetBlockLives(int blockLives)
    {
        this.blockLives = blockLives;
        UpdateVisual() ;
    }



}
