using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  TooltrayTool Class, controls the onscreen tooltray toggle.
    Inherits from Tool class, but overrites some methods. */

public class TooltrayTool : Tool
{
    void Awake()
    {
        functionObject = UIManager.Instance.openedTools;
    }

    public override void ActivateTool()
    {

        // Activate open tooltray
        UIManager.Instance.closedTools.SetActive(false);        
        UIManager.Instance.openedTools.SetActive(true);
        UIManager.Instance.moveButton.SetActive(false);


        if (ModeControl.Instance.isInAR)
        {
            UIManager.Instance.inventoryButton.SetActive(false);
        }

        if (SelectionManager.Instance.robot == null)
        {
            UIManager.Instance.robotTool.SetActive(false);
        }
        else
        {
            if (UIManager.Instance.robotPlusSkeletonTool.activeSelf || UIManager.Instance.skeletonTool.activeSelf)
            {
                UIManager.Instance.robotTool.SetActive(false);
            }            
        }
    }

    public override void DeactivateTool()
    {
        // Deactivate open tooltray
        UIManager.Instance.openedTools.SetActive(false);
        UIManager.Instance.closedTools.SetActive(true);
    }

    public override void CheckIfToolIsActive()
    {
        // Check which object this script is attached to
        if (this.name == "OpenToolsButton") isToolAlreadyActive = false;
        else if (this.name == "CloseToolsButton") isToolAlreadyActive = true;
    }
}
