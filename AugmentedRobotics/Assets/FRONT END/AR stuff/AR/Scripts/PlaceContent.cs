﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceContent : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GraphicRaycaster raycaster;

    private void Update()
    {

        // click should place item in world, unless is over UI
        if (Input.GetMouseButtonDown(0) && !IsClickOverUI())
        {
            List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();

            // perform raycast from mouse position, searching for planes
            raycastManager.Raycast(Input.mousePosition, hitPoints, TrackableType.Planes);

            // if a plane was found, then place object
            if (hitPoints.Count > 0)
            {
                Pose pose = hitPoints[0].pose;
                transform.rotation = pose.rotation;
                transform.position = pose.position;
            }

        }
    }

    bool IsClickOverUI()
    {
        PointerEventData data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);
        return results.Count > 0;
    }

}
