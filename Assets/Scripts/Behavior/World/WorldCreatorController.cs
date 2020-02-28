using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using YamlDotNet.RepresentationModel;

public class WorldCreatorController : MonoBehaviour
{
    public WorldStorageController worldStorageController;
    public int caveSizeBlocks = 10;
    public int pathHeightBlocks = 10;
    public int caveRangeBlocks = 10;
    public int cavesCount = 10;
 
    // Start is called before the first frame update
    void Start()
    {
        //generate empty blocks

        worldStorageController.WorldData = generateWorldData();
    }

    private WorldData generateWorldData()
    {
        WorldData data = new WorldData();
        initBlocks(data);
        initCaves(data, cavesCount);
        return data;
    }

    private void initBlocks(WorldData worldData)
    {
        WorldBlock[,] worldBlocks = new WorldBlock[worldStorageController.worldHeight, worldStorageController.worldWidth];
        int initialX = worldStorageController.initialX;
        int initialY = worldStorageController.initialY;
        float worldWidth = worldStorageController.worldWidth;
        float worldHeight = worldStorageController.worldHeight;
        //generate solid blocks
        for (var i = initialY; i < worldHeight; i++)
        {
            for (var j = initialX; j < worldWidth; j++)
            {
                worldBlocks[i, j] = WorldBlockUtils.getRandomWorldBlock();
            }
        }
        worldData.Blocks = worldBlocks;
    }
    private void initCaves(WorldData worldData, int howMany)
    {
        int cavesLeft = howMany;
        float worldWidth = worldStorageController.worldWidth;
        float worldHeight = worldStorageController.worldHeight;
        Vector2Int? previousCavePosition = null;
        RectInt[] cavesRects = new RectInt[howMany];
        int currentCaveIndex = 0;
        while (cavesLeft > 0)
        {
            int randomI = Random.Range(0, worldData.Blocks.GetLength(0) - caveSizeBlocks);
            int randomJ = Random.Range(0, worldData.Blocks.GetLength(1) - caveSizeBlocks);

            Vector2Int cavePosition = new Vector2Int(randomI, randomJ);
            if (previousCavePosition.HasValue /*&& Vector2Int.Distance(previousCavePosition.Value,cavePosition) < 10*/)
            {
                connectCaves(worldData.Blocks, previousCavePosition.Value, cavePosition);
            }
            generateCave(worldData.Blocks, cavePosition);
            cavesRects[currentCaveIndex] = new RectInt(cavePosition, new Vector2Int(caveSizeBlocks, caveSizeBlocks));
            previousCavePosition = cavePosition;
            cavesLeft--;
            currentCaveIndex++;
        }

    }
    private void connectCaves(WorldBlock[,] blocks, Vector2Int caveA, Vector2Int caveB)
    {
        Vector2Int topCavePosition = caveA.y > caveB.y ? caveA : caveB;
        Vector2Int bottomCavePosition = caveA.y < caveB.y ? caveA : caveB;
        Vector2Int leftCavePosition = caveA.x < caveB.x ? caveA : caveB;
        Vector2Int rightCavePosition = caveA.x > caveB.x ? caveA : caveB;

        for (int i = bottomCavePosition.y + caveSizeBlocks; i < topCavePosition.y; i++)
        {
            for (int j = leftCavePosition.x + caveSizeBlocks; j < rightCavePosition.x; j++)
            {
                Debug.Log("Connecting cave:" + i + "-" + j);
                blocks[i, j] = WorldBlockUtils.getEmptyBlock();
            }
        }
    }
    private void generateCave(WorldBlock[,] blocks, Vector2Int pos)
    {
        for (int i = pos.y; i < pos.y + caveSizeBlocks; i++)
        {
            for (int j = pos.x; j < pos.x + caveSizeBlocks; j++)
            {
                blocks[i, j] = WorldBlockUtils.getEmptyBlock();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



}
