using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DHParameters : MonoBehaviour
{
    /*
       CGB 21/09/20
     
       Method to set up DH parameters from the transformation matrix of a frame.
       It takes two inputs: the list of 4x4 matrices for each successive frame,
       and the effector matrix.
       It outputs a list of Vector4s containing all the parameters for each frame,
       with the parameters of the effector in the last element of the list.
     
       The test checks that each input transformation matrix is valid, and throws
       and exception error otherwise.

     */


    static bool isMValid;
    public static List<Vector4> paramListAllFrames;


    // LINK FRONTEND TO THIS METHOD
    
    // INPUT: transformation matrix list of all frames, and the effector matrix

    public static List<Vector4> GenerateParameterListForAllFrames(Matrix4x4[] successiveMatrices, Matrix4x4 effectorMatrix)
    {
        // For each part of the robot, generate parameter values
        foreach(Matrix4x4 frameMatrix in successiveMatrices)
        {
            // Check if matrix is balid before generating parameters
            if (MatrixValidityTest(frameMatrix) == false)
            {
                throw new System.Exception(frameMatrix + "This transform matrix is invalid.");
            }
            else
            {
                paramListAllFrames.Add(ParametersFromMatrix(frameMatrix));
            }
        }

        // Do the same for the end effector frame
        if (MatrixValidityTest(effectorMatrix) == false)
        {
            throw new System.Exception("The effector matrix is invalid.");
        }
        else
        {
            paramListAllFrames.Add(ParametersFromMatrix(effectorMatrix));
        }

        // OUTPUT: List of all Vector4 parameters, from base at 0 to 
        return paramListAllFrames;

    }


    

    public static bool MatrixValidityTest(Matrix4x4 transformMatrix)
    {
        // TESTING: checks that getting parameters from matrix both ways gives same answer.
        bool isThetaAccurate = (Mathf.Acos(transformMatrix[0, 0]) == Mathf.Asin(transformMatrix[1, 0]));
        bool isAlphaAccurate = (Mathf.Acos(transformMatrix[2, 2]) == Mathf.Asin(transformMatrix[2, 1]));
        bool isAAccurate = (transformMatrix[0, 3] / transformMatrix[0, 0] == transformMatrix[1, 3] / transformMatrix[1, 0]);

        // If false is returned in any of these columns, transform is not valid
        bool[] transformIsValid = new bool[] { isAAccurate, isAlphaAccurate, isThetaAccurate };

        foreach (bool param in transformIsValid)
        {
            if (param == false)
            {
                Debug.Log(param + "is false. The matrix is invalid.");
                isMValid = false;
            }
            else{
                isMValid = true;
            }
        }

        return isMValid;
    }


    public static Vector4 ParametersFromMatrix(Matrix4x4 transformMatrix)
    {
        // Assign each parameter from rotation part of matrix
        float theta = Mathf.Acos(transformMatrix[0, 0]);
        float alpha = Mathf.Acos(transformMatrix[2, 2]);
        float a = transformMatrix[0, 3] / transformMatrix[0, 0];
        float d = transformMatrix[2, 3];

        Vector4 dHParams = new Vector4(a, alpha, d, theta);

        return dHParams;
    }
}
