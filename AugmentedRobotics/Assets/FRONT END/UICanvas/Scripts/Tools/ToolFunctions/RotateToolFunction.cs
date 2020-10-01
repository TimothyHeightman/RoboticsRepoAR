using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RotateToolFunction : Function
{
    //LM 01/10/20
    //Class that handles rotating joints of the articulated body, cannot be used for rotation of the base

    public Robot selectedRobot; //will need to connect this when robot is instantiated from the inventory
    public Joint selectedJoint;
    int selectedJointIndex;

    public Joint movingJoint;

    private void Start()
    {
        selectedJointIndex = 0;     //corresponding to base selected, will not allow any movement        
    }

    private void OnEnable()
    {
        SelectionManager.Instance.rotateToolFunction = this;
        UpdateRefs();
        
        if (selectedJoint != null)
        {
            ToggleMovement(true);
        }
    }

    private void OnDisable()
    {
        if (selectedJoint != null)
        {
            ToggleMovement(false);
        }
        if (movingJoint != null)
        {
            movingJoint.GetComponent<ArticulationJointController> ().enabled = false;
        }
    }

    public void UpdateRefs()
    {
        selectedRobot = SelectionManager.Instance.robot;
        selectedJoint = SelectionManager.Instance.joint;
    }

    int GetIndexFromJoint()
    {
        for (int i = 0; i < selectedRobot.joints.Length - 1; i++)
        {
            if (selectedRobot.joints[i] == selectedJoint)
            {
                return i;
            }
        }
        return 0;
    }

    public void ToggleMovement(bool activate)
    {
        UpdateRefs();
        
        selectedJointIndex = GetIndexFromJoint();
        if (selectedJointIndex != 0)
        {
            Debug.Log(selectedJointIndex);
            selectedRobot.ChangeRotationState(selectedJointIndex, activate);        //This line will trigger the backend
            if (activate)
            {
                movingJoint = selectedJoint;
            }
        }        
    }
}
