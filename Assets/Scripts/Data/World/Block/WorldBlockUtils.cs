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
}
