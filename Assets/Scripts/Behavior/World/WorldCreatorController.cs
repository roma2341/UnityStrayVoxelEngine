using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using YamlDotNet.RepresentationModel;

public class WorldCreatorController : MonoBehaviour
{
    public WorldStorageController worldStorageController;
    public int caveSizeBlocks = 10;
    public int pathHeightBlocks = 5;
    public int pathWidthBlocks = 5;
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
        WorldBlock[,] worldBlocks = new WorldBlock[worldStorageController.worldWidth, worldStorageController.worldHeight];
        int initialX = worldStorageController.initialX;
        int initialY = worldStorageController.initialY;
        float worldWidth = worldStorageController.worldWidth;
        float worldHeight = worldStorageController.worldHeight;
        //generate solid blocks
        for (var i = initialY; i < worldHeight; i++)
        {
            for (var j = initialX; j < worldWidth; j++)
            {
                worldBlocks[j,i] = WorldBlockUtils.getRandomWorldBlock();
            }
        }
        worldData.Blocks = worldBlocks;
    }
    private void initCaves(WorldData worldData, int howMany)
    {
        int cavesLeft = howMany;
        float worldWidth = worldStorageController.worldWidth;
        float worldHeight = worldStorageController.worldHeight;
        worldData.CavesRects = new RectInt[howMany];
        int currentCaveIndex = 0;
        while (cavesLeft > 0)
        {
            int randomY = Random.Range(0, worldData.Blocks.GetLength(0) - caveSizeBlocks);
            int randomX = Random.Range(0, worldData.Blocks.GetLength(1) - caveSizeBlocks);

            Vector2Int cavePosition = new Vector2Int(randomY, randomX);
            generateCave(worldData, cavePosition);
            worldData.CavesRects[currentCaveIndex] =  new RectInt(cavePosition, new Vector2Int(caveSizeBlocks, caveSizeBlocks));
            cavesLeft--;
            currentCaveIndex++;
        }
        connectAllCaves(worldData);
    }
    private void connectAllCaves(WorldData worldData)
    {
        RectInt[] sortedCaves = worldData.getCavesRectsSortedByXY();
        for(int i = 0; i < sortedCaves.Length - 1; i++)
        {
            bool souldConnectWithNext = true;// Random.value > .5;
            if(souldConnectWithNext)
            {
                connectTwoCaves(worldData,sortedCaves[i], sortedCaves[i + 1]);
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

        int rectX = topCaveRect.x + (topCaveRect.width / 2) - (pathWidthBlocks / 2);
        int rectY = bottomCaveRect.y + (bottomCaveRect.height / 2) - (pathHeightBlocks / 2);
        int rectWidth = pathWidthBlocks;
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
         rectY = bottomCaveRect.y + (bottomCaveRect.height / 2) - (pathHeightBlocks / 2);
        if (isTopCaveAtLeftSide)
        {
            //extrude path  to left
            rectWidth =  bottomCaveRect.x - rectX;
        }
        else
        {
            //extrude path to right
            rectWidth = topCaveRect.x + (topCaveRect.width / 2) - rectX;
        }
        rectHeight = pathHeightBlocks;
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
        for (int y = pos.y; y < pos.y + caveSizeBlocks; y++)
        {
            for (int x = pos.x; x < pos.x + caveSizeBlocks; x++)
            {
                worldData.Blocks[x,y] = WorldBlockUtils.getEmptyBlock();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



}
