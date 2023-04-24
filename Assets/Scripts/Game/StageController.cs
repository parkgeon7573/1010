 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    BackgroundBlockSpawner backgroundBlockSpawner;
    [SerializeField]
    BackgroundBlockSpawner foregroundBlockSpawner;
    [SerializeField]
    DragBlockSpawner dragBlockSpawner;
    [SerializeField]
    BlockArrangeSystem blockArrangeSystem;
    [SerializeField]
    UIController uiController;

    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    BackgroundBlock[] backgroundBlocks;
    int currentDragBlockCount;

    readonly Vector2Int blockCount = new Vector2Int(10, 10);
    readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    readonly int maxDragBlockCount = 3;

    List<BackgroundBlock> filledBlockList;

    private void Awake()
    {
        filledBlockList = new List<BackgroundBlock>();
        HighScore = PlayerPrefs.GetInt("HighScore");

        backgroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);

        backgroundBlocks = new BackgroundBlock[blockCount.x * blockCount.y];
        backgroundBlocks = foregroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);

        blockArrangeSystem.Setup(blockCount, blockHalf, backgroundBlocks, this);

        //SpawnDragBlocks();
        StartCoroutine(SpawnDragBlocks());
    }

    IEnumerator SpawnDragBlocks()
    {
        currentDragBlockCount = maxDragBlockCount;
        dragBlockSpawner.SpawnBlocks();
        yield return new WaitUntil(() => IsCompleteSpawnBlocks());
    }
    
    bool IsCompleteSpawnBlocks()
    {
        int count = 0;
        for (int i = 0; i < dragBlockSpawner.BlockSpawnPoints.Length; ++i)
        {
            if(dragBlockSpawner.BlockSpawnPoints[i].childCount != 0&& 
                dragBlockSpawner.BlockSpawnPoints[i].GetChild(0).localPosition == Vector3.zero)
            {
                count++;
            }
        }
        return count == dragBlockSpawner.BlockSpawnPoints.Length;
    }

    
    public void AfterBlockArrangement(DragBlock block)
    {
        StartCoroutine("OnAfterBlockArrangement", block);
    }
    public IEnumerator OnAfterBlockArrangement(DragBlock block)
    { 
        Destroy(block.gameObject);

        int filledLineCount = CheckFilledLine();

        int lineScore = filledLineCount == 0 ? 0 : (int)Mathf.Pow(2, filledLineCount - 1) * 10;

        CurrentScore += block.ChildBlocks.Length + lineScore;

        yield return StartCoroutine(DestroyFilledBlocks(block));

        currentDragBlockCount--;

        if (currentDragBlockCount == 0)
        {
            yield return StartCoroutine(SpawnDragBlocks());
        }
        yield return new WaitForEndOfFrame();

        if (IsGameOver())
        {
            if(CurrentScore > HighScore)
            {
                PlayerPrefs.SetInt("HighScore", CurrentScore);
            }

            uiController.GameOver();
        }
    }

    int CheckFilledLine()
    {
        int filledLintCount = 0;

        filledBlockList.Clear();

        for(int y = 0; y < blockCount.y; ++y)
        {
            int fillBlockCount = 0;
            for(int x = 0; x < blockCount.x; ++x)
            {
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill) fillBlockCount++;
            }

            if(fillBlockCount == blockCount.x)
            {
                for(int x = 0; x < blockCount.x; ++x)
                {
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }
                filledLintCount++;
            }
        }

        for(int x = 0; x < blockCount.x; ++x)
        {
            int fillBlockCount = 0;
            for(int y = 0; y < blockCount.y; ++y)
            {
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill) fillBlockCount++;
            }

            if(fillBlockCount == blockCount.y)
            {
                for(int y = 0; y < blockCount.y; ++y)
                {
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }
                filledLintCount++;
            }
        }
        return filledLintCount;
    }

    private IEnumerator DestroyFilledBlocks(DragBlock block)
    {
        filledBlockList.Sort((a, b) =>
            (a.transform.position - block.transform.position).sqrMagnitude.
            CompareTo((b.transform.position - block.transform.position).sqrMagnitude));
        
        for(int i = 0; i < filledBlockList.Count; ++i)
        {
            filledBlockList[i].EmptyBlock();

            yield return new WaitForSeconds(0.01f);
        }
        filledBlockList.Clear();
    }

    bool IsGameOver()
    {
        int dragBlockCount = 0;

        for (int i = 0; i < dragBlockSpawner.BlockSpawnPoints.Length; ++i)
        {
            if (dragBlockSpawner.BlockSpawnPoints[i].childCount != 0)
            {
                dragBlockCount++;

                if (blockArrangeSystem.IsPossibleArrangement(dragBlockSpawner.BlockSpawnPoints[i].GetComponentInChildren<DragBlock>()))
                {
                    return false;
                }
            }
        }
        return dragBlockCount != 0;
    }
}
