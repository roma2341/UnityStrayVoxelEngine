using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBlock 
{
    SpritesService spritesController;

   public BlockPositionType positionType { get; set; } = BlockPositionType.SNAIL_1;

   public static readonly Dictionary<BlockPositionType, int> SPRITE_INDEX_PER_POSITION_TYPE = new Dictionary<BlockPositionType, int>()
        {
        {BlockPositionType.SNAIL_1,0},
        {BlockPositionType.SNAIL_2,1},
        {BlockPositionType.SNAIL_3,2},
        {BlockPositionType.SNAIL_4,3},
        {BlockPositionType.SNAIL_5,4},
        {BlockPositionType.SNAIL_6,5},
        {BlockPositionType.SNAIL_7,6},
        {BlockPositionType.SNAIL_8,7},
        {BlockPositionType.SNAIL_9,8},
        }; 

    // Start is called before the first frame update
    public void Init(SpritesService controller)
    {
        this.spritesController = controller;
    }

    void Start()
    {
        //SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
       // renderer.sprite = spritesController.getSprite(SPRITE_NAME_PER_POSITION_TYPE[positionType]);
       // Debug.Log("sprite assigned:" + GetComponent<Renderer>().sprite.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
