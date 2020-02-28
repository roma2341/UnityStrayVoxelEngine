using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject target;        //Public variable to store a reference to the player game object
    private Vector3 offset;            
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x,target.transform.position.y,-10);//target.transform.position + offset;
    }
}
