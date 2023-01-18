using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneObjectPositionInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
