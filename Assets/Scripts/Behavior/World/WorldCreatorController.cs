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
        //generate empty blocks
        generateCaves(worldBlocks, cavesCount);
        worldStorageController.blocks = worldBlocks;
        Debug.Log("World blocks were generated:" + worldBlocks);
    }

    private void generateCaves(WorldBlock[,] blocks,int howMany)
    {
        int cavesLeft = howMany;
        float worldWidth = worldStorageController.worldWidth;
        float worldHeight = worldStorageController.worldHeight;
        while (cavesLeft > 0)
        {
            int randomI = Random.Range(0, blocks.GetLength(0) - caveSizeBlocks);
            int randomJ = Random.Range(0, blocks.GetLength(1) - caveSizeBlocks);
            Vector2Int? previousCavePosition = null;
            Vector2Int cavePosition = new Vector2Int(randomI, randomJ);
            if (previousCavePosition.HasValue)
            {
               // connectCaves(previousCavePosition.Value, cavePosition);
            }
            generateCave(blocks, cavePosition);
            cavesLeft--;
        }

    }
    private void connectCaves(WorldBlock[,] blocks,Vector2 caveA, Vector2 CaveB) 
    {
       
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
