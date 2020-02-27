using System.IO;
using UnityEngine;
using YamlDotNet.RepresentationModel;

public class WorldCreatorController : MonoBehaviour
{
    Vector3 previousPosition;
    public SpritesService spritesService;
    // Start is called before the first frame update
    void Start()
    {
        spritesService = new SpritesService();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = new GameObject("New Sprite");
        WorldBlock wb = go.AddComponent<WorldBlock>();
        wb.Init(spritesService);

        var positionTypes = System.Enum.GetValues(typeof(BlockPositionType));
        BlockPositionType randomPositionType = (BlockPositionType)positionTypes.GetValue(Random.Range(0, positionTypes.Length));

        wb.positionType = randomPositionType;

        Vector3 newPosition;
        if (previousPosition == null)
        {
            previousPosition = new Vector3(0, 0, 0);
        }
        newPosition = previousPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); 

        go.transform.position = newPosition;
        previousPosition = newPosition;
      //  Instantiate(go);
    }



}
