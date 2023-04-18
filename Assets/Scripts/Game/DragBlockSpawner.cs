using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    BlockArrangeSystem blockArrangeSystem;
    [SerializeField]
    Transform[] blockSpawnPoints; //드래그 가능한 블록이 배치되는 위치
    [SerializeField]
    GameObject[] blocks;         // 생성 가능한 모든 블록 프리팹
    [SerializeField]
    Vector3 spawnGapAmount = new Vector3(10, 0, 0); // 처음 생성할 때 부모와 떨어진 거리

    public Transform[] BlockSpawnPoints => blockSpawnPoints;
    public void SpawnBlocks()
    {
        StartCoroutine("OnSpawnBlocks");
    }

    IEnumerator OnSpawnBlocks()
    {
        for (int i = 0; i < blockSpawnPoints.Length; ++i)
        {
            yield return new WaitForSeconds(0.1f);
 
            int index = Random.Range(0, blocks.Length - 5);

            Vector3 spawnPosition = blockSpawnPoints[i].position + spawnGapAmount;

            GameObject clone = Instantiate(blocks[index], spawnPosition, Quaternion.identity, blockSpawnPoints[i]);

            clone.GetComponent<DragBlock>().Setup(blockArrangeSystem, blockSpawnPoints[i].position);
        }
    }
}
