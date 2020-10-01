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
        /*if (functionObject == null)
        {
            Transform functionParent = UIManager.Instance.useInAR;
            functionObject = UIManager.Instance.InstantiatePrefab(UIManager.Instance.openedTools, functionParent);
        }*/

    }

    public override void ActivateTool()
    {
        // Activate open tooltray
        SwitchTool();
        UIManager.Instance.closedTools.SetActive(false);
        UIManager.Instance.openedTools.SetActive(true);
    }

    public override void DeactivateTool()
    {
        // Deactivate open tooltray
        UIManager.Instance.openedTools.SetActive(false);
        UIManager.Instance.closedTools.SetActive(true);
        UIManager.Instance.activeTool = null;
    }

    public override void CheckIfToolIsActive()
    {
        // Check which object this script is attached to
        if (this.name == "OpenToolsButton") isToolAlreadyActive = false;
        else if (this.name == "CloseToolsButton"|| this.name == "Overlay") isToolAlreadyActive = true;
    }
}
