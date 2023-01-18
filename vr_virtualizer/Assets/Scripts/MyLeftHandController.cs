using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLeftHandController : MonoBehaviour
{
    private GameObject leftHandController;
    // Start is called before the first frame update
    void Start()
    {
        leftHandController = GameObject.Find("LeftHandAnchor");

    }

    // Update is called once per frame
    void Update()
    {
        if (leftHandController != null)
        {
            transform.localPosition = leftHandController.transform.position;
            transform.localRotation = leftHandController.transform.rotation;
        }
    }
}
