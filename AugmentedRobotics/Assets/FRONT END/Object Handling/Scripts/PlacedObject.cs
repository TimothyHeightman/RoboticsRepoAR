using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{

    [SerializeField]
    private static GameObject objectToPlace;

    [SerializeField]
    private static GameObject placementIndicator;

    private bool IsSelected;

    public bool Selected {
        get {
            return this.IsSelected;
        }
        set {
            IsSelected = value;
        }
    }

}
