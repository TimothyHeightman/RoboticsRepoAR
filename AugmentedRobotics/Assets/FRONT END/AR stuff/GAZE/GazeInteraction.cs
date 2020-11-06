using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GazeInteraction : MonoBehaviour
{
    List<InfoBehaviour> infos = new List<InfoBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAFewSeconds());
        FindInfos();
    }

    IEnumerator WaitAFewSeconds()
    {
        yield return new WaitForSeconds(0.1f);
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
            GameObject target = hit.collider.gameObject;
            
            if (target.CompareTag("hasInfo"))
            {
                float rayLength = Vector3.Magnitude(target.transform.position - transform.position);
                OpenTargetInfo(target.GetComponent<InfoBehaviour>(), rayLength);
            }
            else
            {
                CloseAllInfo();
            }
        }
        else
        {
            CloseAllInfo();
        }
    }

    void OpenTargetInfo(InfoBehaviour targetInfo, float rayLength)
    {
        // loop through all info tags and close all of them except the target
        foreach(InfoBehaviour info in infos)
        {
            if (info == targetInfo)
            {
                info.OpenInfo(rayLength);
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
