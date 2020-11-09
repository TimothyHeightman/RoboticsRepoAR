using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  MoveTool Class, controls the activation of the MoveFunction script.
    Inherits from Tool class. */

public class MoveTool : Tool
{
    [SerializeField] private GameObject moveToolFunctionPrefab;
    public void Initialise()
    {
        Transform functionParent = UIManager.Instance.toolFunctionParent;
        functionObject = UIManager.Instance.InstantiatePrefab(moveToolFunctionPrefab, functionParent);
        SelectionManager.Instance.moveToolFunction = functionObject.GetComponent<MoveToolFunction>();
        functionObject.SetActive(false);
    }
    void Start()
    {
        Initialise();
    }
}
