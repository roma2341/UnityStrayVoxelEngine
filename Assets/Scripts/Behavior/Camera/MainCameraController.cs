using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject target;        //Public variable to store a reference to the player game object
    public bool freeMode = false;
    private Vector3 offset;
    private float zPozition = -10;
    // Start is called before the first frame update
    void Start()
    {
        if (!freeMode)
        {
            offset = transform.position - target.transform.position;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!freeMode)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, zPozition);//target.transform.position + offset;
        }
    }

    private void Update()
    {
        if (freeMode)
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0));
        }
    }
}
