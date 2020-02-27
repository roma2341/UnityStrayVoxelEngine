using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    float hitPoint;
    public void addHitpoint(float amount)
    {
        hitPoint += amount;
    }

    public void decreaseHitpoint(float amount)
    {
        hitPoint -= amount;
    }

    private void destroy()
    {
        Destroy(this);
    }

    public float getHitpoint()
    {
        return hitPoint;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoint <= 0)
        {
            this.destroy();
        }
    }
}
