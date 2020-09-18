using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformMatrixBackend : MonoBehaviour
{
    public List<Transform> currentRobot;
    public Matrix4x4 originMatrix, effectorMatrix;
    //Holds matrices for transforms between successive components
    public Matrix4x4[] sucessiveMatrices;

    public Matrix4x4 testFinal;

    private void Update()
    {
        sucessiveMatrices = new Matrix4x4[currentRobot.Count - 1];
        currentRobot[1].transform.Rotate(Input.GetAxis("Vertical") * Time.deltaTime * 50, Input.GetAxis("Horizontal") * Time.deltaTime * 50, 0);
        GenerateMatrices(currentRobot);

        testFinal = Matrix4x4.identity;
        foreach (var item in sucessiveMatrices)
        {
            testFinal *= item;
        }
        //testFinal *= effectorMatrix;

    }

    public void GenerateMatrices(List<Transform> robotComponents)
    {
        currentRobot = robotComponents;
        GetBaseMatrix(robotComponents[0]);
        if(originMatrix != null && currentRobot.Count > 1)
        {
            GetEffectorMatrix(currentRobot[currentRobot.Count - 1]);

            if (currentRobot.Count > 2)
            {
                GetIntermediateMatrices(currentRobot.GetRange(1, (currentRobot.Count - 1)));
            }

        }
    }

    void GetBaseMatrix(Transform baseTransform)
    {
        originMatrix = Matrix4x4.TRS(baseTransform.position, baseTransform.rotation, baseTransform.localScale);        
    }

    void GetEffectorMatrix(Transform effTransform)
    {
        //First get matrix relative to global origin
        Matrix4x4 globalTransMatrix = Matrix4x4.TRS(effTransform.position, effTransform.rotation, effTransform.localScale);

        //Then use inverse of the base to get matrix relative to the origin of the base
        effectorMatrix = Matrix4x4.Inverse(originMatrix) * globalTransMatrix;
    }

    void GetIntermediateMatrices(List<Transform> middleParts)
    {
        Matrix4x4 lastGlobal = Matrix4x4.identity;
        for (int i = 0; i < middleParts.Count; i++)
        {
            Matrix4x4 globalTransMatrix = Matrix4x4.TRS(middleParts[i].position, middleParts[i].rotation, middleParts[i].localScale);
            if (i == 0)
            {
                sucessiveMatrices[i] = (Matrix4x4.Inverse(originMatrix) * globalTransMatrix);
                lastGlobal = globalTransMatrix;
            }
            else 
            {             
                sucessiveMatrices[i] = (Matrix4x4.Inverse(lastGlobal) * globalTransMatrix);
                lastGlobal = globalTransMatrix;
            }
        }
    }
    

 }

