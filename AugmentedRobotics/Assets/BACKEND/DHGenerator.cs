using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DHGenerator : MonoBehaviour
{
    //Link to backend component of this robot parent GameObject
    public Robot robot;
    public List<Transform> tempFrames;
    public Vector4[] dhParams;
    public Matrix4x4[] matrices;

    void Start()
    {
        robot = this.GetComponent<Robot>();
        tempFrames = robot.parts;
        dhParams = new Vector4[tempFrames.Count - 1];
        matrices = new Matrix4x4[tempFrames.Count - 1];
    }

    public void GenerateAllParameters(List<Transform> frames)
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
        GenerateAllMatrices(frames);
    }

    public void GenerateAllMatrices(List<Transform> frames)
    {
        Matrix4x4 tempMatrix = new Matrix4x4();
        Vector4 param = new Vector4();
        float cAlpha, sAlpha, cTheta, sTheta;

        for (int i = 0; i < frames.Count-1; i++)
        {
            tempMatrix = Matrix4x4.zero;
            param = dhParams[i];
            cAlpha = Mathf.Cos(param.y / 180f);
            sAlpha = Mathf.Sin(param.y / 180f);
            cTheta = Mathf.Cos(param.w / 180f);
            sTheta = Mathf.Sin(param.w / 180f);

            tempMatrix.m00 = cTheta;
            tempMatrix.m01 = -sTheta * cAlpha;
            tempMatrix.m02 = sTheta * sAlpha;
            tempMatrix.m03 = param.x * cTheta;

            tempMatrix.m10 = sTheta;            
            tempMatrix.m11 = cTheta * cAlpha;
            tempMatrix.m12 = -cTheta * sAlpha;
            tempMatrix.m13 = param.x * sAlpha;

            tempMatrix.m21 = sAlpha;
            tempMatrix.m22 = cAlpha;
            tempMatrix.m23 = param.z;

            tempMatrix.m33 = 1f;

            matrices[i] = tempMatrix;
        }
    }




}
