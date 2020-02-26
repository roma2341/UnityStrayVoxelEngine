using System.IO;
using UnityEngine;
using YamlDotNet.RepresentationModel;

public class WorldCreator : MonoBehaviour
{
    Sprite[] sprites;
    Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Tiles/Tiles_0");
        Debug.Log("sprites count:" + sprites.Length);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = new GameObject("New Sprite");
        Vector3 newPosition;
        if (previousPosition == null)
        {
            previousPosition = new Vector3(0, 0, 0);
        }
        newPosition = previousPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); 

        go.transform.position = newPosition;
        previousPosition = newPosition;
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }



}
