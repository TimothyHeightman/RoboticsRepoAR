using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformMatrixBackend : MonoBehaviour
{
    /*
     *LM 15/09/20
     *Set of methods to calculate final and intermediate transformation matrices of a list of connected gameobjects
     *We first get the matrix between the global origin and the base, and use this to get the matrix between the base
     *and the effector, from the matrix between the global origin and the effector
     *Then we calculate matrices between all successive components
     *
     *Also contains supplementary testing materials kept in for convenience in case future changes are made
     *These check whether out effectorMatrix is equal to the product of all of the successive matrices
     */

    //INPUT: Input the currently selected/modified robot as a list of its transforms, ordered in this list as they are in real space (eg base is always element 0)
    List<Transform> currentRobot;

    //Transform matrices of the base  to the global origin, and to the effector from the base
    Matrix4x4 originMatrix, effectorMatrix;

    //Holds matrices for transforms between successive components
    Matrix4x4[] sucessiveMatrices;

    //Used in testing of validity of results (testing purposes)
    Matrix4x4 testFinal;

    void TestAccuracy()
    {
        //TESTING: Used to test validity during the build process

        sucessiveMatrices = new Matrix4x4[currentRobot.Count - 1];

        GenerateMatrices(currentRobot);

        testFinal = Matrix4x4.identity;
        foreach (var item in sucessiveMatrices)
        {
            testFinal *= item;
        }
        Debug.Log("Equal?: " + (testFinal == effectorMatrix))
    }

    //LINK FRONTEND TO THIS METHOD
    //Input: robotComponents - ordered list of transforms that make up the selected robot
    public void GenerateMatrices(List<Transform> robotComponents)
    {
        //NOTE: Call this method from FRONTEND whenever we need to calculate matrices - main method of this script

        currentRobot = robotComponents;
        GetBaseMatrix(robotComponents[0]);
        if(currentRobot.Count > 1)  //If robot has a valid base and effector
        {
            GetEffectorMatrix(currentRobot[currentRobot.Count - 1]);    //Last element in input list

            if (currentRobot.Count > 2)
            {
                GetIntermediateMatrices(currentRobot.GetRange(1, (currentRobot.Count - 1)));    //Calculate matrices between successive components
            }

        }
    }

    void GetBaseMatrix(Transform baseTransform)
    {
        //Produces the transformation matrix from the global frame of the scene to the frame of the base
        //This will be used so that we can get matrices in terms of the base rather than the global frame
        originMatrix = Matrix4x4.TRS(baseTransform.position, baseTransform.rotation, baseTransform.localScale);        
    }

    void GetEffectorMatrix(Transform effTransform)
    {
        //First get matrix relative to global origin
        Matrix4x4 globalTransMatrix = Matrix4x4.TRS(effTransform.position, effTransform.rotation, effTransform.localScale);

        //Then use inverse of the base to get matrix relative to the base
        effectorMatrix = Matrix4x4.Inverse(originMatrix) * globalTransMatrix;
    }

    void GetIntermediateMatrices(List<Transform> middleParts)
    {
        //Generate matrices between successive components
        Matrix4x4 lastGlobal = Matrix4x4.identity;
        for (int i = 0; i < middleParts.Count; i++)
        {
            Matrix4x4 globalTransMatrix = Matrix4x4.TRS(middleParts[i].position, middleParts[i].rotation, middleParts[i].localScale);
            if (i == 0)
            {
                //For first part connected to the base then we need the matrix from the base to middleParts[0]
                sucessiveMatrices[i] = (Matrix4x4.Inverse(originMatrix) * globalTransMatrix);
                lastGlobal = globalTransMatrix;
            }
            else 
            {
                //Otherwise get transformation matrix between parts i and i-1, stored at successiveMatrices[i]
                sucessiveMatrices[i] = (Matrix4x4.Inverse(lastGlobal) * globalTransMatrix);
                lastGlobal = globalTransMatrix;
            }
        }
    }
    

 }

