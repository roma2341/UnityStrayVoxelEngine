using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using YamlDotNet.RepresentationModel;

public class WorldCreatorController : MonoBehaviour
{
    public WorldStorageController worldStorageController;
    // Start is called before the first frame update
    void Start()
    {
         
       WorldBlock[,] worldBlocks = new WorldBlock[worldStorageController.worldHeight, worldStorageController.worldWidth];
        int initialX = worldStorageController.initialX;
        int initialY = worldStorageController.initialY;
        float worldWidth = worldStorageController.worldWidth;
        float worldHeight = worldStorageController.worldHeight;
        for (var i = initialY; i < worldHeight; i++)
        {
            for (var j = initialX; j < worldWidth; j++)
            {
                worldBlocks[i, j] = generateRandomWorldBlock();
            }
        }
        worldStorageController.blocks = worldBlocks;
        Debug.Log("World blocks were generated:" + worldBlocks);
    }

    WorldBlock generateRandomWorldBlock()
    {
        WorldBlock worldBlock = new WorldBlock();
        worldBlock.positionType = getRandomPositionType();
        return worldBlock;
    }

    private BlockPositionType getRandomPositionType()
    {
        var positionTypes = System.Enum.GetValues(typeof(BlockPositionType));
        BlockPositionType randomPositionType = (BlockPositionType)positionTypes.GetValue(Random.Range(0, positionTypes.Length));
        return randomPositionType;
    }

    // Update is called once per frame
    void Update()
    {

    }



}
