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
    private List<Item> items = new List<Item>();

    [SerializeField]
    private Item item;

    public void SpawnBlocks()
    {

        // Shift existing blocks down
        shiftBlocks();

        // Calculate the starting position based on the row width and padding
        float rowWidth = (rowSize - 1) * padding;
        Vector3 startPos = transform.position - Vector3.right * (rowWidth / 2); // Center the row

        int itemIndex = UnityEngine.Random.Range(0, rowSize);

        bool blockSpawned = false;

        // Spawn the blocks with equal spacing
        for (int i = 0; i < rowSize; i++)
        {
            Vector3 pos = startPos + Vector3.right * i * padding;

            if (i == itemIndex)
            {
                // Instantiate an item at this position
                var spawnedItem = Instantiate(item, pos, Quaternion.identity);
                items.Add(spawnedItem);

            }

            if (i != itemIndex && (UnityEngine.Random.Range(0, 100) <= 33 || !blockSpawned))
            {
                // Calculate the position for each block in the row
                var block = Instantiate(blockPrefab, pos, Quaternion.identity);

                int chance = UnityEngine.Random.Range(1,4);

                int score = GameManager.Instance.GetScore;
                int lives = chance < 2 ? (2 * score): score;
                
                block.SetBlockLives(lives);

                blocks.Add(block);
                blockSpawned = true;
            }
        }

        
    }


    private void shiftBlocks()
    {
        blocks.RemoveAll(block => block == null);
        items.RemoveAll(item => item == null);

        foreach (Block block in blocks)
        {
            if (blocks != null)
            {
                block.transform.position += Vector3.down * padding;

                if (block.transform.position.y <= -3)
                {
                    GameManager.Instance.gameover();
                }
            }
        }
        foreach (Item item in items)
        {
            if (item != null)
            {
                item.transform.position += Vector3.down * padding;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 1f);
    }

}