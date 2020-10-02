using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*  Tool Class to provide the default behaviour for buttons. Implements IButtonFunctionality 
    and IPointerDownHandler to deal with click listeners. It also sets out shared methods for
    each tool to activate the functional script when clicked. */

public abstract class Tool : MonoBehaviour, IPointerDownHandler
{
    protected bool isToolAlreadyActive;
    protected GameObject functionObject;

    public virtual void OnPointerDown(PointerEventData data)
    {
        // On button click, check activate or deactivate script depending on if active or not
        CheckIfToolIsActive();

        if (isToolAlreadyActive)
        {
            DeactivateTool();

        } else if (!isToolAlreadyActive)
        {
            ActivateTool();
        }
    }

    public virtual void CheckIfToolIsActive()
    {
        // Check if function script is already active
        if (functionObject.activeSelf == false) isToolAlreadyActive = false;
        else if (functionObject.gameObject.activeSelf == true) isToolAlreadyActive = true;
    }

    public virtual void DeactivateTool()
    {
        // Disable tool highlight
        this.transform.GetChild(0).gameObject.SetActive(false);

        // Disable the function script
        UIManager.Instance.activeTool = null;
        functionObject.SetActive(false);
    }

    public virtual void ActivateTool()
    {
        // Enable tool highlight
        this.transform.GetChild(0).gameObject.SetActive(false);

        // Enable the function script
        SwitchTool();
        functionObject.SetActive(true);
    }

    public virtual void SwitchTool()
    {
        if (UIManager.Instance.activeTool != null)
        {
            UIManager.Instance.activeTool.DeactivateTool();
        }
        UIManager.Instance.activeTool = this;
    }

}
