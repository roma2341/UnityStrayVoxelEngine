using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class WorldStorageController : MonoBehaviour
{
    private WorldData WorldData { get; set; } = new WorldData();
    public Tilemap tileMap { get; set; }


    private WorldConfig config;
    private Tile[] groundTiles;

    private AbstractWorldDataHandler[] worldDataHandlers;
    private void Start()
    {
        config = GetComponent<WorldConfig>();
        groundTiles = Resources.LoadAll<Tile>("Tiles/Ground");
        Transform tileMapTransform = transform.Find("BlockTilemap");
        if (tileMapTransform == null)
        {
            Debug.LogError("Tilemap not found !");
        }
        tileMap = tileMapTransform.gameObject.GetComponent<Tilemap>();
        worldDataHandlers = new AbstractWorldDataHandler[]{
            new BlocksGenerator(config),
            new CavesGenerator(config)
        };
        foreach (AbstractWorldDataHandler worldDataHandler in worldDataHandlers)
        {
            worldDataHandler.handle(WorldData);
        }
    }
    public void Update()
    {
        for (var y = config.initialY; y < config.worldHeight; y++)
        {
            for (var x = config.initialX; x < config.worldWidth; x++)
            {
                WorldBlock block = WorldData.Blocks[x, y];
                BlockPositionType positionType = block.PositionType;
                int tileIndex = WorldBlock.SPRITE_INDEX_PER_POSITION_TYPE[positionType];
                Tile tile = null;
                if (block.BlockType == BlockType.GROUND)
                {
                    tile = groundTiles[tileIndex];
                }
                 tileMap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
  
    }

    private void mutateRandomBlock()
    {
        var i = Random.Range(0, WorldData.Blocks.GetLength(0));
        var j = Random.Range(0, WorldData.Blocks.GetLength(1));
        WorldData.Blocks[i, j].PositionType = WorldBlockUtils.getRandomPositionType();

    }
}
