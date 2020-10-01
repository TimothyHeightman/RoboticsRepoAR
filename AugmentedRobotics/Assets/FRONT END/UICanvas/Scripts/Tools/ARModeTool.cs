﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  ARModeTool Class, controls the entering AR environment. Inherits from Tool class.
    Will need further functionality added in connecting to the AR*/

public class ARModeTool : Tool
{
    void Awake()
    {
        functionObject = UIManager.Instance.homeScreen;
        /*if (functionObject == null)
        {
            Transform functionParent = UIManager.Instance.useOutsideAR;
            functionObject = UIManager.Instance.InstantiatePrefab(UIManager.Instance.homeScreen, functionParent);
        }*/
    }

    public override void ActivateTool()
    {
        // Activate AR Mode (remove homeScreen, activate AR)
        UIManager.Instance.homeScreen.SetActive(false);
    }

    public override void DeactivateTool()
    {
        // Deactivate AR mode (add homeScreen, deactivate AR)
        UIManager.Instance.homeScreen.SetActive(true);
    }

    public override void CheckIfToolIsActive()
    {
        // Check which object this script is attached to
        if (this.name == "ARButton") isToolAlreadyActive = false;
        else if (this.name == "ExitARButton") isToolAlreadyActive = true;
    }
}