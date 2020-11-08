using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  SkeletonControlTool Class, controls the activation of the SkeletonControlFunction script.
    Inherits from Tool class. */

public class SkeletonControlTool : Tool
{
    [SerializeField] private GameObject skeletonControlToolFunctionPrefab;

    void Awake()
    {
        Transform functionParent = UIManager.Instance.toolFunctionParent;
        Transform functionTransform = functionParent.Find("SkeletonControlToolFunction");

        if(functionTransform == null)
        {
            functionObject = UIManager.Instance.InstantiatePrefab(skeletonControlToolFunctionPrefab, functionParent);
        }
        else
        {
            functionObject = functionTransform.gameObject;
        }
        
        functionObject.SetActive(false);
    }
}
