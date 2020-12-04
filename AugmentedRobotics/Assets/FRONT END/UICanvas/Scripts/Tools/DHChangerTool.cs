using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  InventoryTool Class, controls the activation of the InventoryFunction script.
    Inherits from Tool class. */

public class DHChangerTool : Tool
{
    [SerializeField] private GameObject dhChangerModalPrefab;

    void Start()
    {
        Initialise();
    }

    public void Initialise()
    {
        Transform functionParent = UIManager.Instance.modalsParent;
        functionObject = UIManager.Instance.InstantiatePrefab(dhChangerModalPrefab, functionParent);
        functionObject.SetActive(false);
    }
}