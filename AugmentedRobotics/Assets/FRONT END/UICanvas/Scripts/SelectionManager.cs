using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionManager : MonoBehaviour
{
    private static SelectionManager _instance;
    public Camera cam;

    //Selection manager as singleton
    public static SelectionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ObjectManager is NULL.");
            }

            return _instance;
        }
    }

    public Robot robot;
    public Joint joint;
    public Joint previous;

    public RotateToolFunction rotateToolFunction;
    public MoveToolFunction moveToolFunction;


    private void Awake()
    {
        _instance = this;
        cam = Camera.main;
        previous = null;
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Get user input based on click from camera in game view
        RaycastHit hit;                                    //Currently calling this every frame for debugging purposes
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Joint>() != null)
                {
                    if (previous != null)
                    {
                        if (previous != hitObject.GetComponent<Joint>())
                        {
                            if (previous.GetComponent<ArticulationJointController>() != null)
                            {
                                previous.GetComponent<ArticulationJointController>().enabled = false;
                            }
                            
                        }
                    }

                    joint = hitObject.GetComponent<Joint>();
                    previous = joint;

                    if (rotateToolFunction != null)
                    {
                        if (rotateToolFunction.enabled == true)
                        {
                            joint.GetComponent<ArticulationJointController>().enabled = true;
                        }
                    }

                    
                }
            }           
        }
    }


}
