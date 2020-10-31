using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class GazeInteraction : MonoBehaviour
{
    List<InfoBehaviour> infos = new List<InfoBehaviour>();
    public TextMeshProUGUI text; 


    // Start is called before the first frame update
    void Start()
    {
        FindInfos();
    }

    private void FindInfos()
    {
        infos = FindObjectsOfType<InfoBehaviour>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        FindInfos();
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            text.text = "hit: " + hit.collider.gameObject.tag;
            GameObject target = hit.collider.gameObject;
            if (target.CompareTag("hasInfo"))
            {
                OpenTargetInfo(target.GetComponent<InfoBehaviour>());
            }
        }
        else
        {
            CloseAllInfo();
        }
    }

    void OpenTargetInfo(InfoBehaviour targetInfo)
    {
        // loop through all info tags and close all of them except the target
        foreach(InfoBehaviour info in infos)
        {
            if(info == targetInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
    }

    void CloseAllInfo()
    {
        foreach(InfoBehaviour info in infos)
        {
            info.CloseInfo();
        }
    }
}
