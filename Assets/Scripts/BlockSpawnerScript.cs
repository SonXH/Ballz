using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Block blockPrefab;

    private int rowSize = 7;
    private float padding = 0.8f;

    private List<Block> blocks = new List<Block>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBlocks()
    {
        Debug.Log("spawning");

        // Shift existing blocks down
        shiftBlocks();

        // Calculate the starting position based on the row width and padding
        float rowWidth = (rowSize - 1) * padding;
        Vector3 startPos = transform.position - Vector3.right * (rowWidth / 2); // Center the row

        // Spawn the blocks with equal spacing
        for (int i = 0; i < rowSize; i++)
        {
            if (UnityEngine.Random.Range(0, 100) <= 33)
            {
                // Calculate the position for each block in the row
                Vector3 pos = startPos + Vector3.right * i * padding;

                var block = Instantiate(blockPrefab, pos, Quaternion.identity);

                int chance = UnityEngine.Random.Range(1,3);

                int score = GameManager.Instance.GetScore;
                int lives = chance < 2 ? (2 * score): score;
                
                block.SetBlockLives(lives);

                blocks.Add(block);
            }
        }
    }


    private void shiftBlocks()
    {
        blocks.RemoveAll(block => block == null);
        
        foreach (var block in blocks)
        {
            if (blocks != null)
            {
                block.transform.position += Vector3.down * padding;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 1f);
    }

}