using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySecondVRUserController : MonoBehaviour
{
    private GameObject vrUserController;
    // Start is called before the first frame update
    void Start()
    {
        vrUserController = GameObject.Find("CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        if (vrUserController != null)
        {
            transform.localPosition = vrUserController.transform.position - new Vector3(0.0f, 0.0f, 0.0f);
            transform.localRotation = vrUserController.transform.rotation;
        }
    }
}
