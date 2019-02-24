using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handtracker : MonoBehaviour
{
    public GameObject tracker;
    public float multiplier = 2;
    public Vector3 offset = new Vector3(-0.72f, 14, -2.9f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Transform location of cube based on hand position
        Vector3 use = new Vector3(
            -tracker.transform.position.x * multiplier + offset.x, 
            tracker.transform.position.z * multiplier + offset.y, 
            0);
        this.transform.SetPositionAndRotation(use, tracker.transform.rotation);
    }
}
