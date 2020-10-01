using System;
using System.Collections;
using System.Collections.Generic;
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

    //We make use of a robot class attached to the same robot parent object, that stores the transforms of the child joints

    //Transform matrices of the base  to the global origin, and to the effector from the base
    public Matrix4x4 originMatrix, effectorMatrix;

    //Holds matrices for transforms between successive components
    public Matrix4x4[] sucessiveMatrices;

    //Used in testing of validity of results (testing purposes)
    Matrix4x4 testFinal;

    //Link to backend component of this robot parent GameObject
    public Robot robot;

    public Matrix4x4 OriginMatrix { get => originMatrix; set => originMatrix = value; }
    public Matrix4x4 EffectorMatrix { get => effectorMatrix; set => effectorMatrix = value; }
    public Matrix4x4[] SucessiveMatrices { get => sucessiveMatrices; set => sucessiveMatrices = value; }

    public List<Vector4> paramListAllFrames;


    public void Initialise()
    {
        robot = this.GetComponent<Robot>();
        Debug.Log(robot.parts.Count);
        SucessiveMatrices = new Matrix4x4[robot.parts.Count - 1];
        GenerateAllMatrices();
    }

    

    void TestAccuracy()
    {
        //TESTING: Used to test validity during the build process+

        SucessiveMatrices = new Matrix4x4[robot.parts.Count - 1];

        GenerateAllMatrices();

        testFinal = Matrix4x4.identity;
        foreach (var item in SucessiveMatrices)
        {
            testFinal *= item;
        }
        Debug.Log("Equal?: " + (testFinal == EffectorMatrix));
    }

    //LINK FRONTEND TO THIS METHOD
    //Input: robotComponents - ordered list of transforms that make up the selected robot
    public void GenerateAllMatrices()
    {
        //NOTE: Call this method from FRONTEND whenever we need to calculate matrices - main method of this script

        UpdateBaseMatrix(robot.parts[0]);
        if(robot.parts.Count > 1)  //If robot has a valid base and effector
        {
            UpdateEffectorMatrix(robot.parts[robot.parts.Count - 1]);    //Last element in input list

            if (robot.parts.Count > 2)
            {
                UpdateIntermediateMatrices(robot.parts.GetRange(1, (robot.parts.Count - 1)));    //Calculate matrices between successive components
            }

        }

        //paramListAllFrames = DHParameters.GenerateParameterListForAllFrames(SucessiveMatrices, EffectorMatrix);
    }

    public void UpdateSelectedMatrices(int movedJointIndex)
    {
        //Generates/Updates all matrices from the index provided, working up the robot until the effector is reached
        if (movedJointIndex != (robot.parts.Count-1) )      //If we have moved not just the effector
        {
            UpdateIntermediateMatrices(robot.parts.GetRange(movedJointIndex, (robot.parts.Count - movedJointIndex)));
        }

        UpdateEffectorMatrix(robot.parts[robot.parts.Count - 1]); 
    }

    void UpdateBaseMatrix(Transform baseTransform)
    {
        //Produces the transformation matrix from the global frame of the scene to the frame of the base
        //This will be used so that we can get matrices in terms of the base rather than the global frame

        OriginMatrix = Matrix4x4.TRS(baseTransform.position, baseTransform.rotation, Vector3.one);        
    }

    void UpdateEffectorMatrix(Transform effectorTransform)
    {

        //First get matrix relative to global origin
        Matrix4x4 globalTransMatrix = Matrix4x4.TRS(effectorTransform.position, effectorTransform.rotation, Vector3.one);

        //Then use inverse of the base to get matrix relative to the base
        EffectorMatrix = Matrix4x4.Inverse(OriginMatrix) * globalTransMatrix;

        //Debug.Log(effectorTransform.position);
    }

    void UpdateIntermediateMatrices(List<Transform> middleParts)
    {
        //Generate matrices between successive components
        Matrix4x4 lastGlobal = Matrix4x4.identity;
        for (int i = 0; i < middleParts.Count; i++)
        {
            Matrix4x4 globalTransMatrix = Matrix4x4.TRS(middleParts[i].position, middleParts[i].rotation, Vector3.one);
            if (i == 0)
            {
                //For first part connected to the base then we need the matrix from the base to middleParts[0]
                SucessiveMatrices[i] = (Matrix4x4.Inverse(OriginMatrix) * globalTransMatrix);
                lastGlobal = globalTransMatrix;
            }
            else 
            {
                //Otherwise get transformation matrix between parts i and i-1, stored at successiveMatrices[i]
                SucessiveMatrices[i] = (Matrix4x4.Inverse(lastGlobal) * globalTransMatrix);
                lastGlobal = globalTransMatrix;
            }
        }
    }
    

 }

