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

    public Material tempMaterial;




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
    }

    private void Update()
    {
        Ray ray = SelectionManager.Instance.cam.ScreenPointToRay(Input.mousePosition); //Get user input based on click from camera in game view
        RaycastHit hit;                                    //Currently calling this every frame for debugging purposes
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Joint>() != null)
                {
                    if (SelectionManager.Instance.previous != null)
                    {
                        if (SelectionManager.Instance.previous != hitObject.GetComponent<Joint>())
                        {
                            if (SelectionManager.Instance.previous.GetComponent<ArticulationJointController>() != null)
                            {
                                ToggleOutline(SelectionManager.Instance.previous, false);

                                SelectionManager.Instance.previous.GetComponent<ArticulationJointController>().enabled = false;
                            }

                        }
                    }

                    SelectionManager.Instance.joint = hitObject.GetComponent<Joint>();
                    SelectionManager.Instance.previous = SelectionManager.Instance.joint;

                    UpdateRefs();

                    if (SelectionManager.Instance.joint.GetComponent<ArticulationJointController>() != null)
                    {
                        SelectionManager.Instance.joint.GetComponent<ArticulationJointController>().enabled = true;
                        ToggleOutline(SelectionManager.Instance.joint, true);
                    }                    
                }
            }
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
                selectedJoint.GetComponent<Renderer>().materials[1] = SelectionManager.Instance.outline;
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
        Material[] materials;

        if (isEnabled)
        {
            materials = new Material[2];
            materials[0] = renderer.materials[0];
            materials[1] = SelectionManager.Instance.outline;
        }
        else
        {
            materials = new Material[1];
            materials[0] = renderer.materials[0];
        }

        joint.GetComponent<Renderer>().materials = materials;
    }
}
