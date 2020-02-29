using UnityEngine;
using System.Collections;

public class WorldData
{
   public RectInt[] CavesRects { get; set; }
   public WorldBlock[,] Blocks { get; set; }
   public RectInt[] getCavesRectsSortedByXY()
    {
        System.Comparison<RectInt> comparsion = (rectA, rectB) =>
        {
            int firstCompare = rectA.x.CompareTo(rectB.x);
            return firstCompare != 0 ? firstCompare : rectA.y.CompareTo(rectB.y);
        };
        System.Array.Sort(CavesRects, comparsion);
        return CavesRects;
    }
}
