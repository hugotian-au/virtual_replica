using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateKinectPosition : MonoBehaviour
{
    public Vector3 registeredPosition = new Vector3(1.677976f, 2.498745f, 1.319135f);
    public Quaternion registeredRotatoin = Quaternion.Euler(151.641f, 30.689f, 0.9599919f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = registeredPosition;
        transform.localRotation = registeredRotatoin;
    }
}
