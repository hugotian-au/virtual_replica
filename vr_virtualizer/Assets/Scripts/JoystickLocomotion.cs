using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class JoystickLocomotion : MonoBehaviour
{
    // public GameObject player;
    public float speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var joystickAxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick, OVRInput.Controller.LTouch);
        float fixedY = transform.position.y;
        transform.position += (transform.right * joystickAxis.x + transform.forward * joystickAxis.y) * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
    }
}
