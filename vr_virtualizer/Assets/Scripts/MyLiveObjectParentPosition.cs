using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLiveObjectParentPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Matrix4x4 OriginPos = Matrix4x4.identity;
    public Matrix4x4 Matrix1;
    public Matrix4x4 Matrix2;
    public Matrix4x4 Matrix3;
    public Matrix4x4 MatrixPose;

    void Start()
    {
        // Don't change this, Camera1.Position
        Matrix1.SetTRS(new Vector3(1.6089f, 2.636f, 1.268833f),
                       Quaternion.Euler(-30.512f, 30.318f, -0.189f),
                       new Vector3(1, 1, 1));
        // Camera2.Position
        //Matrix2.SetTRS(new Vector3(-1.2798654f, 0.8009058f, 1.2420996f),
        //               Quaternion.Euler(-1.877354f, 89.60242f, -63.634f),
        //               new Vector3(1, 1, 1));
        Matrix2.SetTRS(new Vector3(1.664f, 2.620f, -0.87f),
                       Quaternion.Euler(-30.005f, 142.615f, 0.113f),
                       new Vector3(1, 1, 1));
        // Camera3.Position
        //Matrix3.SetTRS(new Vector3(1.7427824f, 1.2180851f, 2.1037217f),
        //               Quaternion.Euler(5.35802f, -71.978f, 45.8632f),
        //               new Vector3(1, 1, 1));
        Matrix3.SetTRS(new Vector3(-1.62f, 2.615f, 1.252968f),
               Quaternion.Euler(-26.074f, -53.43f, -0.7220001f),
               new Vector3(1, 1, 1));
        if (gameObject.name == "ak_content_0")
        {
            MatrixPose = Matrix1 * OriginPos; 
        }
        else if (gameObject.name == "ak_content_1")
        {
            MatrixPose = Matrix2 *  OriginPos;
        }
        else if(gameObject.name == "ak_content_2")
        {
            MatrixPose = Matrix3 * OriginPos;
        }


        transform.localPosition = new Vector3(MatrixPose.m03, MatrixPose.m13, MatrixPose.m23);
        float qw = Mathf.Sqrt(1f + MatrixPose.m00 + MatrixPose.m11 + MatrixPose.m22) / 2;
        float w = 4 * qw;
        float qx = (MatrixPose.m21 - MatrixPose.m12) / w;
        float qy = (MatrixPose.m02 - MatrixPose.m20) / w;
        float qz = (MatrixPose.m10 - MatrixPose.m01) / w;
        transform.localRotation = new Quaternion(qx, qy, qz, qw);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
