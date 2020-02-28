using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class WorldStorageController : MonoBehaviour
{

    public int worldWidth;
    public int worldHeight;
    public int initialX = 0;
    public int initialY = 0;
    public WorldBlock[,] blocks { get; set; }
    public Tile[] groundTiles;
    public Tilemap tileMap { get; set; }
    private void Start()
    {
        groundTiles = Resources.LoadAll<Tile>("Tiles/Ground");
        Transform tileMapTransform = transform.Find("BlockTilemap");
        if (tileMapTransform == null)
        {
            Debug.LogError("Tilemap not found !");
        }
        tileMap = tileMapTransform.gameObject.GetComponent<Tilemap>();
    }
    public void Update()
    {
        for (var i = initialY; i < worldHeight; i++)
        {
            for (var j = initialX; j < worldWidth; j++)
            {
                WorldBlock block = blocks[i, j];
                BlockPositionType positionType = block.PositionType;
                int tileIndex = WorldBlock.SPRITE_INDEX_PER_POSITION_TYPE[positionType];
                Tile tile = null;
                if (block.BlockType == BlockType.GROUND)
                {
                    tile = groundTiles[tileIndex];
                }
                 tileMap.SetTile(new Vector3Int(j, i, 0), tile);
            }
        }
  
    }

    private void mutateRandomBlock()
    {
        var i = Random.Range(0, blocks.GetLength(0));
        var j = Random.Range(0, blocks.GetLength(1));
        blocks[i, j].PositionType = WorldBlockUtils.getRandomPositionType();

    }
}
