using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBlock : MonoBehaviour
{
    SpritesService spritesController;

   public BlockPositionType positionType { get; set; } = BlockPositionType.SNAIL_1;

   private static readonly Dictionary<BlockPositionType, string> SPRITE_NAME_PER_POSITION_TYPE = new Dictionary<BlockPositionType, string>()
        {
        {BlockPositionType.SNAIL_1,"Tiles_0_0"},
        {BlockPositionType.SNAIL_2,"Tiles_0_1"},
        {BlockPositionType.SNAIL_3,"Tiles_0_2"},
        {BlockPositionType.SNAIL_4,"Tiles_0_3"},
        {BlockPositionType.SNAIL_5,"Tiles_0_4"},
        {BlockPositionType.SNAIL_6,"Tiles_0_5"},
        {BlockPositionType.SNAIL_7,"Tiles_0_6"},
        {BlockPositionType.SNAIL_8,"Tiles_0_7"},
        {BlockPositionType.SNAIL_9,"Tiles_0_8"},
        }; 

    // Start is called before the first frame update
    public void Init(SpritesService controller)
    {
        this.spritesController = controller;
    }

    void Start()
    {
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = spritesController.getSprite(SPRITE_NAME_PER_POSITION_TYPE[positionType]);
        Debug.Log("sprite assigned:" + renderer.sprite.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
