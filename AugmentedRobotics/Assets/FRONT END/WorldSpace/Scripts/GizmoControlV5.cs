﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoControlV5 : MonoBehaviour
{
    // Get relevant variables from other scripts
    private Robot robot;
    private DHGenerator dHGenerator;
    private Joint[] joints;
    private List<Transform> parts;


    // Get prefabs for gizmos
    [SerializeField] GameObject axesGizmoPrefab;
    [SerializeField] GameObject rotationGizmoPrefab;
    [SerializeField] GameObject dhGizmoPrefab;

    // Store useful variables
    private GameObject axesGizmo;
    private GameObject dhGizmo;
    private GameObject rotationGizmo;
    private bool isGizmoNeeded;


     void Start()
    {
        // Get relevant scripts for transforms and parenting
        robot = GetComponent<Robot>();
        dHGenerator = GetComponent<DHGenerator>();

        SetupAllGizmos();
    }

    void SetupAllGizmos()
    {
        for (int i = 0; i < robot.joints.Length; i++)
        {
            InstantiateGizmoPrefabs(i, robot.joints[i], rotationGizmo, rotationGizmoPrefab, "RotationGizmo");
            InstantiateGizmoPrefabs(i, robot.joints[i], axesGizmo, axesGizmoPrefab, "AxesGizmo");
            //SetUpGizmos(i, part, part.dhGizmo, dhGizmoPrefab, "DHParamGizmo");
        }
    }

    private void InstantiateGizmoPrefabs(int i, Joint joint, GameObject gizmo, GameObject gizmoPrefab, string gizmoName)
    {
        // Only generate gizmos when needed
        CheckIfRotationGizmoActive(i, gizmoPrefab);
        
        if (isGizmoNeeded)
        {
            // Instantiate gizmo at DH parameters place
            gizmo = Instantiate(gizmoPrefab);
            gizmo.transform.parent = robot.joints[i].robotPart.transform;
            gizmo.transform.rotation = robot.parts[i].rotation;
            gizmo.transform.position = robot.parts[i].position;
            gizmo.name = gizmoName;
            gizmo.tag = "hasInfo";

            if(gizmoPrefab == rotationGizmoPrefab)
            {
                // Fourth frame placed rotation gizmo at wrong height
                gizmo.transform.position = robot.joints[i].marker.transform.position;
            }
        }
    }

    private void CheckIfRotationGizmoActive(int i, GameObject gizmoPrefab)
    {
        // If base or end effector, return false (rotation gizmo not needed)
        bool isRotationGizmo = gizmoPrefab == rotationGizmoPrefab;
        bool isNotBaseRotation = isRotationGizmo && i!=0;
        bool isNotEffectorRotation = isRotationGizmo && i!=(robot.joints.Length-1);
        bool isNotEndsRotation = isNotBaseRotation && isNotEffectorRotation;
        
        isGizmoNeeded = !isRotationGizmo || isNotEndsRotation;
    }

}
