using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DHGenerator : MonoBehaviour
{
    //Link to backend component of this robot parent GameObject
    public Robot robot;
    public List<Transform> frames;
    public Vector4[] dhParams;
    public Matrix4x4[] matrices;

    void Start()
    {
        robot = this.GetComponent<Robot>();
        frames = robot.parts;
        dhParams = new Vector4[frames.Count - 1];
        matrices = new Matrix4x4[frames.Count - 1];
    }

    public void GenerateAllParameters()
    {
        for (int i = 0; i < frames.Count - 1; i++)
        {
            Vector4 tempParams = Vector4.zero;            
            float a, d, alpha, theta;
            Vector3 displacement = new Vector3();
            displacement = frames[i+1].position - frames[i].position;

            a = Vector3.Dot(displacement, frames[i].right);
            d = Vector3.Dot(displacement, frames[i].forward);

            alpha = Vector3.SignedAngle(frames[i].forward, frames[i + 1].forward, frames[i].right);
            if (alpha < 0)
            {
                alpha += 360f;
            }

            theta = Vector3.SignedAngle(frames[i].right, frames[i+1].right, frames[i+1].forward);
            if (theta < 0)
            {
                theta += 360f;
            }

            tempParams.w = theta;
            tempParams.x = a;
            tempParams.y = alpha;
            tempParams.z = d;
            

            dhParams[i] = tempParams;
        }
    }

    public void GenerateAllMatrices()
    {

    }




}
