using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    BlockArrangeSystem blockArrangeSystem;
    [SerializeField]
    Transform[] blockSpawnPoints; //�巡�� ������ ����� ��ġ�Ǵ� ��ġ
    [SerializeField]
    GameObject[] blocks;         // ���� ������ ��� ��� ������
    [SerializeField]
    Vector3 spawnGapAmount = new Vector3(10, 0, 0); // ó�� ������ �� �θ�� ������ �Ÿ�

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
