using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRightHandController : MonoBehaviour
{
    private GameObject rightHandController;
    // Start is called before the first frame update
    void Start()
    {
        rightHandController = GameObject.Find("RightHandAnchor");

    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandController != null)
        {
            transform.localPosition = rightHandController.transform.position;
            transform.localRotation = rightHandController.transform.rotation;
        }
    }
}
