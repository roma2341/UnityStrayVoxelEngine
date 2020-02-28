using UnityEngine;
using System.Collections;

public class WorldBlockUtils
{
    public static BlockPositionType getRandomPositionType()
    {
        var positionTypes = System.Enum.GetValues(typeof(BlockPositionType));
        BlockPositionType randomPositionType = (BlockPositionType)positionTypes.GetValue(Random.Range(0, positionTypes.Length));
        return randomPositionType;
    }
    private static BlockType getRandomBlockType()
    {
        var blockTypes = System.Enum.GetValues(typeof(BlockPositionType));
        BlockType randomBlockType = (BlockType)blockTypes.GetValue(Random.Range(0, blockTypes.Length));
        return randomBlockType;
    }
    public static WorldBlock getRandomWorldBlock()
    {
        WorldBlock worldBlock = new WorldBlock();
        worldBlock.PositionType = getRandomPositionType();
        worldBlock.BlockType = getRandomBlockType();
        return worldBlock;
    }
}
