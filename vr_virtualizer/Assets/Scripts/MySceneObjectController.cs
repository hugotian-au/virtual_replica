using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MySceneObjectController : MonoBehaviourPunCallbacks, IPunObservable
{
    private GameObject tracker;
    private Vector3 position;
    private Quaternion rotation;
    private Rigidbody rb;
    private bool IsKinematicSet = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        position = rb.position;
        rotation = rb.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            position = rb.position;
            rotation = rb.rotation;
            // transform.parent.position = transform.position - transform.localPosition;
            // transform.parent.rotation = transform.rotation - transform.localRotation;
            if (!IsKinematicSet)
            {
                var rb = GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.detectCollisions = true;
                rb.WakeUp();

                IsKinematicSet = true;
            }
        }
        else
        {
            rb.MovePosition(position);
            rb.MoveRotation(rotation.normalized);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(position);
            stream.SendNext(rotation);
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
