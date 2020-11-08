using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum DHparameter
{
    a,
    alpha,
    d,
    theta
}


[System.Serializable]
public class DHFrame
{
    // Class to define all of the parameters and their respecitve textboxes
    public List<DHparameter> parameterList = new List<DHparameter>(4);
    public List<TextMeshProUGUI> orderedTextboxList = new List<TextMeshProUGUI>(4);

    public DHFrame(List<DHparameter> parameterList, List<TextMeshProUGUI> orderedTextboxList)
    {
        this.parameterList = parameterList;
        this.orderedTextboxList = orderedTextboxList;
    }
}