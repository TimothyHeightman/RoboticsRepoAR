using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InfoBehaviour_NonTiered : MonoBehaviour
{
    const float SPEED = 6f;

    [SerializeField] public List<Transform> InfoItems;
    [SerializeField] float offDistance = 1f;

    //keeps track of the current tier
    // tier 0 corresponds to no info
    // tier 1 corresponds to the next layer, etc.
    bool isActive = false;
    Vector3 zeroScale = Vector3.zero; //default size = 0
    Vector3 maxScale = Vector3.one;

    void ProcessTiers(List<Transform> InfoItems)
    {
        Debug.Log("Processing Tiers");
        Debug.Log("InfoTiers Length: " + InfoItems.Count);

        if (isActive)
        {
            foreach (Transform tier in InfoItems)
            {
                tier.gameObject.SetActive(true);
                tier.localScale = Vector3.Lerp(tier.localScale, maxScale, Time.deltaTime * SPEED);
            }
        }
    }

    public void OpenInfo(float rayLength)
    {
        isActive = CheckValid(rayLength);
        Debug.Log("gaze activated? :  " + isActive);
        ProcessTiers(InfoItems);
    }

    public void CloseInfo()
    {
        isActive = false;
        ProcessTiers(InfoItems);
    }


    public void AddItem(Transform item)
    {
        InfoItems.Add(item);
    }

    bool CheckValid(float length)
    {
        if (length < offDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
