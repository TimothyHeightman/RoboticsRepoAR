using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionManagerAR : MonoBehaviour
{
    private static SelectionManagerAR _instance;
    public Camera cam;
    public Material outline;

    //Selection manager as singleton
    public static SelectionManagerAR Instance
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

    public RobotAR robot;
    public Joint joint;
    public Joint previous;

    public RotateToolFunctionAR rotateToolFunction;
    public MoveToolFunction moveToolFunction;


    private void Awake()
    {
        _instance = this;
        cam = Camera.main;
        previous = null;
    }
}
