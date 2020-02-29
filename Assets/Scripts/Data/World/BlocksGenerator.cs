using UnityEngine;
using System.Collections;

public class BlocksGenerator : AbstractWorldDataHandler
{
    public BlocksGenerator(WorldConfig config) : base(config)
    {
     
    }

    public override void handle(WorldData data)
    {
        initBlocks(data);
    }

    private void initBlocks(WorldData worldData)
    {
        WorldBlock[,] worldBlocks = new WorldBlock[config.worldWidth, config.worldHeight];
        int initialX = config.initialX;
        int initialY = config.initialY;
        float worldWidth = config.worldWidth;
        float worldHeight = config.worldHeight;
        //generate solid blocks
        for (var i = initialY; i < worldHeight; i++)
        {
            for (var j = initialX; j < worldWidth; j++)
            {
                worldBlocks[j, i] = WorldBlockUtils.getRandomWorldBlock();
            }
        }
        worldData.Blocks = worldBlocks;
    }
}
