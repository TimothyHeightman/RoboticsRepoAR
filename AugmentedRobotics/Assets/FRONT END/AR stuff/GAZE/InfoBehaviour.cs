using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InfoBehaviour : MonoBehaviour
{
    const float SPEED = 6f;

    [SerializeField] List<Transform> InfoTiers;

    [SerializeField] float offDistance;
    [SerializeField] float tierOneDistance;
    [SerializeField] float tierTwoDistance;

    //keeps track of the current tier
    // tier 0 corresponds to no info
    // tier 1 corresponds to the next layer, etc.
    int currentTier = 0;
    Vector3 zeroScale = Vector3.zero; //default size = 0
    Vector3 maxScale = Vector3.one;

    void ProcessTiers(List<Transform> InfoTiers)
    {
        Debug.Log("Processing Tiers");
        Debug.Log("InfoTiers Length: " + InfoTiers.Count);
        int tierIndex = 1;
        foreach(Transform tier in InfoTiers)
        {
            // open the current tier
            if (tierIndex == currentTier)
            {
                tier.gameObject.SetActive(true);
                tier.localScale = Vector3.Lerp(tier.localScale, maxScale, Time.deltaTime * SPEED);
            }
            // close all other tiers
            else
            {
                tier.localScale = Vector3.Lerp(tier.localScale, zeroScale, Time.deltaTime * SPEED);
                tier.gameObject.SetActive(false);
            }

            //iterate tier
            tierIndex += 1;
        }
    }

    public void OpenInfo(float rayLength)
    {
        currentTier = CheckTier(rayLength);
        Debug.Log("Current Tier: " + currentTier);
        ProcessTiers(InfoTiers);
    }

    public void CloseInfo()
    {
        currentTier = 0;
        ProcessTiers(InfoTiers);
    }

    int CheckTier(float length)
    {
        if(length > offDistance)
        {
            return 0;
        }
        else if (offDistance > length && length > tierOneDistance)
        {
            return 1;
        }
        else if (tierOneDistance > length && length > tierTwoDistance)
        {
            return 2;
        }
        else
        {
            return 9;
        }
    }
}
