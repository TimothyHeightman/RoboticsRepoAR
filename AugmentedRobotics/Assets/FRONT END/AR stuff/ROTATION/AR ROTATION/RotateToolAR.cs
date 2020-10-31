using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  RotateTool Class, controls the activation of the RotateFunction script.
    Inherits from Tool class. */

public class RotateToolAR : Tool
{
    [SerializeField] private GameObject rotateToolFunctionPrefab;

    void Awake()
    {
        Transform functionParent = UIManager.Instance.toolFunctionParent;
        functionObject = UIManager.Instance.InstantiatePrefab(rotateToolFunctionPrefab, functionParent);
        functionObject.SetActive(false);
    }

}
