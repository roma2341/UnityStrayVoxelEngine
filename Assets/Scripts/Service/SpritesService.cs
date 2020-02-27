using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpritesService 
{
    protected Dictionary<string,Sprite> sprites;
    public SpritesService()
    {
        var spritesArray = Resources.LoadAll<Sprite>("Tiles/Ground");
        sprites = spritesArray.ToDictionary(item => item.name,
                                       item => item);
    }

    public Sprite getSprite(string name)
    {
        return sprites[name];
    }
}
