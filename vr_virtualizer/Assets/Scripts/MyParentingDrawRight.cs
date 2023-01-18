using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParentingDrawRight : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("TrackingSpace");
        transform.parent = parent.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
