using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Joint[] joints;
    public TransformMatrixBackend matrixBackend;
    public List<Transform> parts;

    //Need to fill in array of joints, call this on startup

    private void Start()
    {
        matrixBackend = this.GetComponent<TransformMatrixBackend>();

        UpdatePartsTransforms();      
    }

     void UpdatePartsTransforms()
    {
        //Updates the contents of parts which is used by the backend for matrix calculations
        parts = new List<Transform>();

        foreach (var joint in joints)
        {
            parts.Add(joint.robotPart.transform);
        }
    }

    public void ChangeRotationState(int jointIndex, bool isMovable)
    {      
        if (!joints[jointIndex].isBase)
        {
            ArticulationJointController jointController = joints[jointIndex].jointController;
            jointController.enabled = isMovable;    //OnEnable and FixedUpdate methods of jointController will now run for movement
            if (!isMovable)
            {
                UpdateMatrices(jointIndex); //If we have finished moving an update then recalc matrices, if we want to do this during motion call this inside AJC
            }
        }        
    }

    private void UpdateMatrices(int jointIndex = 0)
    {
        //By default update all matrices, if not then update all matrices after and including the index of the joint passed in
        if (jointIndex == 0)
        {
            matrixBackend.GenerateAllMatrices();
        }
        else
        {
            matrixBackend.UpdateSelectedMatrices(jointIndex);
        }
    }



    //HELPERS

    public void StopAllMotion()
    {
        //Supplementary method, used in the demo before every movement but not truly implemented
        //Kept here if discovered needed during testing - on disable method of AJC should cover it

        foreach (var joint in joints)
        {
            if (!joint.isBase)
            {
                joint.jointController.StopRotation();
            }            
        }
    }

}
