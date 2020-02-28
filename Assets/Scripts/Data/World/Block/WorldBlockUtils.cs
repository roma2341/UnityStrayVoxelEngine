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
    private static BlockType getRandomBlockType(bool includeEmpty = false)
    {
        var blockTypes = System.Enum.GetValues(typeof(BlockType));
        int initialBlockIndex = includeEmpty ? 0 : 1;
        BlockType randomBlockType = (BlockType)blockTypes.GetValue(Random.Range(initialBlockIndex, blockTypes.Length));
        return randomBlockType;
    }
    public static WorldBlock getRandomWorldBlock()
    {
        WorldBlock worldBlock = new WorldBlock();
        worldBlock.PositionType = getRandomPositionType();
        worldBlock.BlockType = getRandomBlockType();
        return worldBlock;
    }
    public static WorldBlock getEmptyBlock()
    {
        WorldBlock worldBlock = new WorldBlock();
        worldBlock.BlockType = BlockType.EMPTY;
        return worldBlock;
    }
}
