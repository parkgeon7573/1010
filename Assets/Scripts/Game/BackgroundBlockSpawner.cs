using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBlockSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject block;
    [SerializeField]
    int orderInLayer;

    /*Vector2Int blockCount = new Vector2Int(10, 10);
    Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    private void Awake()
    {
        for(int y =0; y < blockCount.y; ++y)
        {
            for(int x =0; x < blockCount.x; ++x)
            {
                float px = -blockCount.x * 0.5f + blockHalf.x + x;
                float py = blockCount.y * 0.5f - blockHalf.y - y;
                Vector3 position = new Vector3(px, py, 0);

                GameObject clone = Instantiate(block, position, Quaternion.identity, transform);

                clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            }
        }
    }*/

    public BackgroundBlock[] SpawnBlocks(Vector2Int blockCount, Vector2 blockHalf)
    {
        BackgroundBlock[] blocks = new BackgroundBlock[blockCount.x * blockCount.y];

        for(int y = 0; y < blockCount.y; ++y)
        {
            for(int x = 0; x < blockCount.x; ++x)
            {
                float px = -blockCount.x * 0.5f + blockHalf.x + x;
                float py = blockCount.y * 0.5f - blockHalf.y - y;
                Vector3 position = new Vector3(px, py, 0);

                GameObject clone = Instantiate(block, position, Quaternion.identity, transform);

                clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;

                blocks[y * blockCount.x + x] = clone.GetComponent<BackgroundBlock>();
            }
        }
        return blocks;
    }
}
