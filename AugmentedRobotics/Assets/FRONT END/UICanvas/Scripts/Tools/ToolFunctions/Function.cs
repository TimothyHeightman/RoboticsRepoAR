using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Function : MonoBehaviour
{
    public virtual void OnDisable()
    {
        UIManager.Instance.activeTool = null;
    }
}
