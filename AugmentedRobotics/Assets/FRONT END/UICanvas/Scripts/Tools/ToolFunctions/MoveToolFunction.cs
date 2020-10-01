using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToolFunction : Function
{
    //LM 01/10/20
    //Class that handles moving base of the articulated body, cannot be used for movement of joints

    public Robot selectedRobot; //will need to connect this when robot is instantiated from the inventory
    public Joint selectedJoint;
    int selectedJointIndex;

    private void Start()
    {
        selectedJointIndex = 1;     //corresponding to joint that isnt base        
    }

    private void OnEnable()
    {
        SelectionManager.Instance.moveToolFunction = this;
        UpdateRefs();
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
        return 1;
    }

    public void ToggleMovement(bool activate)
    {
        UpdateRefs();

        GetIndexFromJoint();
        if (selectedJointIndex == 0)
        {
            Debug.Log("Movement active:" + activate + " (not yet implemented)");     //This line will trigger the backend
        }
    }
}
