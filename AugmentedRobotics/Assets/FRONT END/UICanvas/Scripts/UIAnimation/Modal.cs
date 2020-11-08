using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Modal : MonoBehaviour
{   
    // Define all variables to be inherited by Modal children
    protected GameObject modal;
    protected GameObject activeBckg;
    protected Button modalOverlay;
    protected Button closeModalBtn;
    //private GameObject inventoryToolHighlight;

    void Awake ()
    {
        // Define all of the variables required, inherited from parent Modal class
        modal = this.gameObject;
        modalOverlay = this.transform.GetChild(0).GetComponent<Button>();
        CloseListeners();
    }

    public virtual void CloseModal()
    {
        // Deactivate modal and active tool cue
        modal.SetActive (false);
        UIManager.Instance.activeTool = null;

        /*if(this.name = )
        {
            inventoryToolHighlight.SetActive(false);
        }*/
    }

    public virtual void CloseListeners()
    {
        // Add listeners and close modal
        modalOverlay.onClick.AddListener( delegate{ CloseModal(); });
        if (closeModalBtn != null)
        {
            closeModalBtn.onClick.AddListener( delegate{ CloseModal(); });
        }
    }
}
