using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Joint[] joints;
    public DHGenerator dhGenerator;
    public List<Transform> parts;
    public GameObject markerPrefab;


    //Need to fill in array of joints, call this on startup

    private void Start()
    {
        SelectionManager.Instance.robot = this;

        dhGenerator = this.GetComponent<DHGenerator>();

        SetupMarkers();

        UpdatePartsTransforms();

        //matrixBackend.Initialise();
    }

    private void Update()
    {
        //UpdateMatrices();
        dhGenerator.GenerateAllParameters();
    }

    void SetupMarkers()
    {
        Quaternion previousRotation = Quaternion.identity;
        for (int i = 0; i < joints.Length; i++)
        {
            Joint joint = joints[i];
            joint.Setup();
            //joints[i].marker = Instantiate(markerPrefab);
            joint.marker = new GameObject();   
            joint.marker.transform.parent = joints[i].robotPart.transform;
            joint.PlaceMarker(previousRotation);
            previousRotation = joint.marker.transform.rotation;

            joint.dhFrame = new GameObject();
            joint.dhFrame.transform.parent = joints[i].robotPart.transform;
            joint.dhFrame.transform.rotation = joint.marker.transform.rotation;
            joint.dhFrame.transform.position = joint.marker.transform.position;
            joint.dhFrame.name = "DH Frame";

            if (i > 1 && i < joints.Length - 1)     //special cases here for Franka - base and effector always skipped, first element has parallel z axis so we skip
            {
                joint.PlaceDHFrame(joint.dhFrame, joints[i - 1].dhFrame);
            }

            GameObject.Destroy(joint.marker);       //Removes old joint markers for clarity
        }
    }

     void UpdatePartsTransforms()
    {
        //TODO
        //Updates the contents of parts which is used by the backend for matrix calculations, these need to be the marker transforms
        parts = new List<Transform>();

        for (int i = 0; i < joints.Length; i++)
        {
            parts.Add(joints[i].dhFrame.transform);
        }
    }

    public void ChangeRotationState(int jointIndex, bool isMovable)
    {      
        if (!joints[jointIndex].isBase || !joints[jointIndex].isEnd)
        {
            ArticulationJointController jointController = joints[jointIndex].jointController;
            jointController.enabled = isMovable;    //OnEnable and FixedUpdate methods of jointController will now run for movement
            Debug.Log(isMovable);
            if (!isMovable)
            {
                //UpdateMatrices(jointIndex); //If we have finished moving an update then recalc matrices, if we want to do this during motion call this inside AJC
            }
        }        
    }

    //private void UpdateMatrices(int jointIndex = 0)
    //{
    //    //matrixBackend.GenerateAllMatrices();

    //    //By default update all matrices, if not then update all matrices after and including the index of the joint passed in - MAY NOT BE NEEDED
    //    //if (jointIndex == 0)
    //    //{
    //    //    matrixBackend.GenerateAllMatrices();
    //    //}
    //    //else
    //    //{
    //    //    matrixBackend.UpdateSelectedMatrices(jointIndex);
    //    //}
    //}



    //HELPERS

    public void StopAllMotion()
    {
        //Supplementary method, used in the demo before every movement but not truly implemented
        //Kept here if discovered needed during testing - on disable method of AJC should cover it

        foreach (var joint in joints)
        {
            if (!joint.isBase || !joint.isEnd)
            {
                joint.jointController.StopRotation();
            }            
        }
    }

}
