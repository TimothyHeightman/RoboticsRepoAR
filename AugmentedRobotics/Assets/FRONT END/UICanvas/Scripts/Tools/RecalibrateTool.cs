using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  RecalibrateTool Class, controls the activation of the RecalibrateFunction script.
    Inherits from Tool class. */

public class RecalibrateTool : Tool
{
    [SerializeField] private GameObject recalibrateToolFunctionPrefab;

    void Awake()
    {
        Transform functionParent = UIManager.Instance.toolFunctionParent;
        functionObject = UIManager.Instance.InstantiatePrefab(recalibrateToolFunctionPrefab, functionParent);
        functionObject.SetActive(false);
    }
}
