﻿using System.IO;
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
                worldBlocks[i, j] = WorldBlockUtils.getRandomWorldBlock();
            }
        }
        worldStorageController.blocks = worldBlocks;
        Debug.Log("World blocks were generated:" + worldBlocks);
    }

    // Update is called once per frame
    void Update()
    {

    }



}
