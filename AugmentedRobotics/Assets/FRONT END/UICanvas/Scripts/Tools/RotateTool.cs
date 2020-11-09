using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  RotateTool Class, controls the activation of the RotateFunction script.
    Inherits from Tool class. */

public class RotateTool : Tool
{
    [SerializeField] private GameObject rotateToolFunctionPrefab;

    void Start()
    {
        Transform functionParent = UIManager.Instance.toolFunctionParent;
        functionObject = UIManager.Instance.InstantiatePrefab(rotateToolFunctionPrefab, functionParent);
        SelectionManager.Instance.rotateToolFunction = functionObject.GetComponent<RotateToolFunction>();
        functionObject.SetActive(false);
    }

}
