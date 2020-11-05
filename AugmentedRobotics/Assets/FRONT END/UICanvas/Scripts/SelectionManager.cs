using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionManager : MonoBehaviour
{
    private static SelectionManager _instance;
    public Camera cam;
    public Material outline;

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
    public MoveTool moveTool;

    private void Start()
    {
        moveTool.enabled = true;
        moveTool.Initialise();
        moveTool.enabled = false;
    }


    private void Awake()
    {
        _instance = this;
        cam = Camera.main;
        previous = null;
    }
}
