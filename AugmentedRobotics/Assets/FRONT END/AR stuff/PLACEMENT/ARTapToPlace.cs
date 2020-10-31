using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using System;

public class ARTapToPlace : MonoBehaviour
{
    public GameObject placementIndicator;
    private ARSessionOrigin AR_Origin;
    private ARRaycastManager AR_Raycast;
    private Pose placementPose;
    private bool poseValid = false;

    // Start is called before the first frame update
    void Start()
    {
        AR_Origin = FindObjectOfType<ARSessionOrigin>();
        AR_Raycast = AR_Origin.GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if (poseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        AR_Raycast.Raycast(screenCenter, hits, TrackableType.Planes);

        poseValid = hits.Count > 0;
        if (poseValid)
        {
            placementPose = hits[0].pose;
        }

    }
}

