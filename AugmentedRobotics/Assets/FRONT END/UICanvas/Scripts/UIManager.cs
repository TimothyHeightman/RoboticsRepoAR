﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {   get
    {
            if (instance == null)
                // Test 1
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    // Test 2
                    Debug.LogError("UIManager is NULL.");
                }

            return instance;

        }
    }

    [Header("To Be Assigned")]
    [SerializeField] public GameObject openedTools;
    [SerializeField] public GameObject closedTools;
    [SerializeField] public GameObject homeScreen;
    [SerializeField] public Transform toolFunctionParent;
    [SerializeField] public Transform meshParent;
    [SerializeField] public Transform arUI;
    

    [Header("No Assignment Needed")]
    public Tool activeTool;

    public Transform canvas;
    public Transform useInAR;
    public Transform useOutsideAR;
    public Transform modalsParent;

    private void Start()
    {
        UpdateRefs();
    }

    private void Awake()
    {
        instance = this; 
        canvas = this.transform.GetChild(0);
        useInAR = canvas.GetChild(0);
        useOutsideAR = canvas.GetChild(1);
        modalsParent = canvas.GetChild(2);
    }

    private void UpdateRefs()
    {
        canvas = this.transform.GetChild(0);
        useInAR = canvas.GetChild(0);
        useOutsideAR = canvas.GetChild(1);
        modalsParent = canvas.GetChild(2);
    }

    public GameObject InstantiatePrefab(GameObject prefab, Transform parent)
    {
        UpdateRefs();
        GameObject newObject = Instantiate(prefab);
        newObject.name = prefab.name;
        newObject.transform.SetParent(parent, false);

        return newObject;
    }

}
