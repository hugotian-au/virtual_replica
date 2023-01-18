using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyInitBlockPosition : MonoBehaviour
{
    private Vector3 blockPosition1 = new Vector3(1.187f, 1.484f, -1.209f);
    private Quaternion blockRotatoin1 = Quaternion.Euler(-184.02f, 358.052f, -180.674f);

    private Vector3 blockPosition2 = new Vector3(1.014f, 1.601f, -1.223f);
    private Quaternion blockRotatoin2 = Quaternion.Euler(-1.796f, -4.257f, -267.993f);

    private Vector3 blockPosition3 = new Vector3(0.4674166f, 1.615f, -1.317f);
    private Quaternion blockRotatoin3 = Quaternion.Euler(1.283f, -260.66f, 176.602f);

    private Vector3 blockPosition4 = new Vector3(0.488f, 1.161f, -1.297f);
    private Quaternion blockRotatoin4 = Quaternion.Euler(0f, -85.86501f, 0f);

    private Vector3 blockPosition5 = new Vector3(0.6696568f, 1.617383f, -1.253379f);
    private Quaternion blockRotatoin5 = Quaternion.Euler(-87.79501f, 91.09701f, -353.182f);

    private Vector3 blockPosition6 = new Vector3(1.226f, 1.164f, -1.187f);
    private Quaternion blockRotatoin6 = Quaternion.Euler(0f, 92.313f, 0f);

    private Vector3 blockPosition7 = new Vector3(0.863f, 1.189f, -1.166f);
    private Quaternion blockRotatoin7 = Quaternion.Euler(-2.297f, -2.759f, 178.738f);

    private bool updated = false;
    // Start is called before the first frame update
    void Start()
    {
        // var pv = GetComponent<PhotonView>();
        // pv.RequestOwnership();

        if (gameObject.name == "block1(Clone)")
        {
            transform.localPosition = blockPosition1;
            transform.localRotation = blockRotatoin1;
        }
        else if (gameObject.name == "block2(Clone)")
        {
            transform.localPosition = blockPosition2;
            transform.localRotation = blockRotatoin2;
        }
        else if (gameObject.name == "block3(Clone)")
        {
            transform.localPosition = blockPosition3;
            transform.localRotation = blockRotatoin3;
        }
        else if (gameObject.name == "block4(Clone)")
        {
            transform.localPosition = blockPosition4;
            transform.localRotation = blockRotatoin4;
        }
        else if (gameObject.name == "block5(Clone)")
        {
            transform.localPosition = blockPosition5;
            transform.localRotation = blockRotatoin5;
        }
        else if (gameObject.name == "block6(Clone)")
        {
            transform.localPosition = blockPosition6;
            transform.localRotation = blockRotatoin6;
        }
        else if (gameObject.name == "block7(Clone)")
        {
            transform.localPosition = blockPosition7;
            transform.localRotation = blockRotatoin7;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!updated)
        {
            if (gameObject.name == "block1(Clone)")
            {
                transform.localPosition = blockPosition1;
                transform.localRotation = blockRotatoin1;
            }
            else if (gameObject.name == "block2(Clone)")
            {
                transform.localPosition = blockPosition2;
                transform.localRotation = blockRotatoin2;
            }
            else if (gameObject.name == "block3(Clone)")
            {
                transform.localPosition = blockPosition3;
                transform.localRotation = blockRotatoin3;
            }
            else if (gameObject.name == "block4(Clone)")
            {
                transform.localPosition = blockPosition4;
                transform.localRotation = blockRotatoin4;
            }
            else if (gameObject.name == "block5(Clone)")
            {
                transform.localPosition = blockPosition5;
                transform.localRotation = blockRotatoin5;
            }
            else if (gameObject.name == "block6(Clone)")
            {
                transform.localPosition = blockPosition6;
                transform.localRotation = blockRotatoin6;
            }
            else if (gameObject.name == "block7(Clone)")
            {
                transform.localPosition = blockPosition7;
                transform.localRotation = blockRotatoin7;
            }

            updated = true;
        }
    }
}
