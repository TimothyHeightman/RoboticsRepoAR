using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionManager : MonoBehaviour
{
    private static SelectionManager instance;
    public Camera cam;
    public Material outline;

    //Selection manager as singleton
    public static SelectionManager Instance
    {
        get
        {   
            if (instance == null)
                // Test 1
                instance = FindObjectOfType<SelectionManager>();

                if (instance == null)
                {
                    // Test 2
                    Debug.LogError("SelectionManager is NULL.");
                }

            return instance;
        }
    }

    public Robot robot;
    public Joint joint;
    public Joint previous;

    public RotateToolFunction rotateToolFunction;
    public MoveToolFunction moveToolFunction;
    public MoveTool moveTool;
    public InventoryTool inventoryTool;
    public InventoryToolFunction InventoryToolFunction;

    public ModeControl modeControl;
    public GameObject openTools;

    IEnumerator Init()
    {
        //Coroutine run fron initialisation of scene to access references of tools
        openTools.SetActive(true);
        yield return new WaitForSeconds(.1f);
        openTools.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine("Init");
    }

    public void Initialise()
    {
        openTools.SetActive(false);
    }


    private void Awake()
    {
        instance = this;
        cam = Camera.main;
        previous = null;
    }
}