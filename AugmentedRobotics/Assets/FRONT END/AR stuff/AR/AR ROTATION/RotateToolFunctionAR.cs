using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RotateToolFunctionAR : Function
{
    //LM 01/10/20
    //Class that handles rotating joints of the articulated body, cannot be used for rotation of the base

    public RobotAR selectedRobot; //will need to connect this when robot is instantiated from the inventory
    public Joint selectedJoint;
    int selectedJointIndex;

    public Material tempMaterial;




    private void Start()
    {
        selectedJointIndex = 0;     //corresponding to base selected, will not allow any movement        
    }

    private void OnEnable()
    {
        SelectionManagerAR.Instance.rotateToolFunction = this;
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
    }

    private void Update()
    {
        Ray ray = SelectionManagerAR.Instance.cam.ScreenPointToRay(Input.mousePosition); //Get user input based on click from camera in game view
        RaycastHit hit;                                    //Currently calling this every frame for debugging purposes
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Joint>() != null)
                {
                    if (SelectionManagerAR.Instance.previous != null)
                    {
                        if (SelectionManagerAR.Instance.previous != hitObject.GetComponent<Joint>())
                        {
                            if (SelectionManagerAR.Instance.previous.GetComponent<ArticulationJointControllerAR>() != null)
                            {
                                ToggleOutline(SelectionManagerAR.Instance.previous, false);

                                SelectionManagerAR.Instance.previous.GetComponent<ArticulationJointControllerAR>().enabled = false;
                            }

                        }
                    }

                    SelectionManagerAR.Instance.joint = hitObject.GetComponent<Joint>();
                    SelectionManagerAR.Instance.previous = SelectionManagerAR.Instance.joint;

                    UpdateRefs();

                    if (SelectionManagerAR.Instance.joint.GetComponent<ArticulationJointControllerAR>() != null)
                    {
                        SelectionManagerAR.Instance.joint.GetComponent<ArticulationJointControllerAR>().enabled = true;
                        ToggleOutline(SelectionManagerAR.Instance.joint, true);
                    }                    
                }
            }
        }
    }

    public void UpdateRefs()
    {
        selectedRobot = SelectionManagerAR.Instance.robot;
        selectedJoint = SelectionManagerAR.Instance.joint;
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
                selectedJoint.GetComponent<Renderer>().materials[1] = SelectionManagerAR.Instance.outline;
            }
            else
            {
                selectedJoint.GetComponent<Renderer>().materials[1] = null;
            }
        }        
    }

    void ToggleOutline(Joint joint, bool isEnabled)
    {
        Renderer renderer = joint.gameObject.GetComponent<Renderer>();
        Material[] materials = new Material[2];
        materials[0] = renderer.materials[0];

        if (isEnabled)
        {
            materials[1] = SelectionManagerAR.Instance.outline;
        }

        joint.GetComponent<Renderer>().materials = materials;
    }
}
