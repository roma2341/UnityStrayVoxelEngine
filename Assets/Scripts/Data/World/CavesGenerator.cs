using UnityEngine;
using System.Collections;
public class CavesGenerator : AbstractWorldDataHandler
{

    public CavesGenerator(WorldConfig config) : base(config)
    {
        
    }
    public override void handle(WorldData data)
    {
        initCaves(data);
    }
    private void initCaves(WorldData worldData)
    {
        int cavesLeft = config.cavesCount;
        float worldWidth = config.worldWidth;
        float worldHeight = config.worldHeight;
        worldData.CavesRects = new RectInt[config.cavesCount];
        int currentCaveIndex = 0;
        while (cavesLeft > 0)
        {
            int randomY = Random.Range(0, worldData.Blocks.GetLength(0) - config.caveSizeBlocks);
            int randomX = Random.Range(0, worldData.Blocks.GetLength(1) - config.caveSizeBlocks);

            Vector2Int cavePosition = new Vector2Int(randomY, randomX);
            generateCave(worldData, cavePosition);
            worldData.CavesRects[currentCaveIndex] = new RectInt(cavePosition, new Vector2Int(config.caveSizeBlocks, config.caveSizeBlocks));
            cavesLeft--;
            currentCaveIndex++;
        }
        connectAllCaves(worldData);
    }
    private void connectAllCaves(WorldData worldData)
    {
        RectInt[] sortedCaves = worldData.getCavesRectsSortedByXY();
        for (int i = 0; i < sortedCaves.Length - 1; i++)
        {
            bool souldConnectWithNext = true;// Random.value > .5;
            if (souldConnectWithNext)
            {
                connectTwoCaves(worldData, sortedCaves[i], sortedCaves[i + 1]);
            }
        }
    }
    private void connectTwoCaves(WorldData worldData, RectInt caveA, RectInt caveB)
    {
        RectInt topCaveRect = caveA.y > caveB.y ? caveA : caveB;
        RectInt bottomCaveRect = caveA.y < caveB.y ? caveA : caveB;
        bool isTopCaveAtLeftSide = topCaveRect.x < bottomCaveRect.x;
        // RectInt leftCaveRect = caveA.x < caveB.x ? caveA : caveB;
        //  RectInt rightCaveRect = caveA.x > caveB.x ? caveA : caveB;

        /*Draw this part (marked with 1)
 .:-------------:`                                                              .:-------------:`   
 :.             /.                                                              :-             /.   
 :.             /+------------------::                     /--------------------s-             /.   
 :.             //                  ./                     o                    o-             /.   
 :.             //                  ./                     o                    o-             /.   
 :.             /+--------------+/::/o                     -+-----+-------------o-             /.   
 ::....-------..+`              1    1                      1     1             -:.....-----.../.   
  `````s...../:``               1    1                      1     1              ``````s---s````    
       1      1                 1    1                      1     1                    1   1        
       1      1                 1`   1                      1     1                    1   1        
       1      1           -/----/::::/---/.            `+---+:::::/---:-               1   1        
       1      1           :-             :.            `/             ./               1   1        
       1      1-----------o-             :.            `/             .h---------------/---/-/      
       1`````o-           h-             :.            `/             .d                     o      
       ------::-----------o-             :.            `/             .h---------------------/      
                          :-             :.            `/             ./                            
                          ./-------------/.            `/-------------:-  
         */

        int rectX = topCaveRect.x + (topCaveRect.width / 2) - (config.pathWidthBlocks / 2);
        int rectY = bottomCaveRect.y + (bottomCaveRect.height / 2) - (config.pathHeightBlocks / 2);
        int rectWidth = config.pathWidthBlocks;
        int rectHeight = topCaveRect.y - rectY;
        RectInt pathRect = new RectInt(rectX, rectY, rectWidth, rectHeight);
        for (int y = pathRect.y; y < pathRect.yMax; y++)
        {
            for (int x = pathRect.x; x < pathRect.xMax; x++)
            {
                worldData.Blocks[x, y] = WorldBlockUtils.getEmptyBlock();
            }
        }

        /*Draw this part (marked with 1)
 .:-------------:`                                                              .:-------------:`   
 :.             /.                                                              :-             /.   
 :.             /+111111111111111111::                     /11111111111111111111s-             /.   
 :.             //                  ./                     o                    o-             /.   
 :.             //                  ./                     o                    o-             /.   
 :.             /+11111111111111+/::/o                     1+11111+1111111111111o-             /.   
 ::....-------..+`              o    o                      o     o             -:.....-----.../.   
  `````s...../:``               o    o                      o     o              ``````s---s````    
       o     :-                 o    o                      o     o                    o   o        
       o     :-                 o`   o                      o     o                    o   o        
       o     :-           -/----/::::/---/.            `+---+:::::/---:-               o   o        
       o     :-           :-             :.            `/             ./               o   o        
       o     //11111111111o-             :.            `/             .h111111111111111/---/-/      
       o`````o-           h-             :.            `/             .d                     o      
       ------::11111111111o-             :.            `/             .h111111111111111111111/      
                          :-             :.            `/             ./                            
                          ./-------------/.            `/-------------:-  
    */

        rectX = isTopCaveAtLeftSide ? topCaveRect.x + (topCaveRect.width / 2) : bottomCaveRect.xMax;
        rectY = bottomCaveRect.y + (bottomCaveRect.height / 2) - (config.pathHeightBlocks / 2);
        if (isTopCaveAtLeftSide)
        {
            //extrude path  to left
            rectWidth = bottomCaveRect.x - rectX;
        }
        else
        {
            //extrude path to right
            rectWidth = topCaveRect.x + (topCaveRect.width / 2) - rectX;
        }
        rectHeight = config.pathHeightBlocks;
        pathRect = new RectInt(rectX, rectY, rectWidth, rectHeight);
        for (int y = pathRect.y; y < pathRect.yMax; y++)
        {
            for (int x = pathRect.x; x < pathRect.xMax; x++)
            {
                worldData.Blocks[x, y] = WorldBlockUtils.getEmptyBlock();
            }
        }

    }
    private void generateCave(WorldData worldData, Vector2Int pos)
    {
        for (int y = pos.y; y < pos.y + config.caveSizeBlocks; y++)
        {
            for (int x = pos.x; x < pos.x + config.caveSizeBlocks; x++)
            {
                worldData.Blocks[x, y] = WorldBlockUtils.getEmptyBlock();
            }
        }
    }

}
