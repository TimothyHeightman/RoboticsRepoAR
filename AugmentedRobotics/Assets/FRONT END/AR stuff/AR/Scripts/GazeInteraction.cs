using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GazeInteraction : MonoBehaviour
{
    List<InfoBehaviour> infos = new List<InfoBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        infos = FindObjectsOfType<InfoBehaviour>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
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
