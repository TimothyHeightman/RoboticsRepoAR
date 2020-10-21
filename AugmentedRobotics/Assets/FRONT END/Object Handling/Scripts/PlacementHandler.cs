using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementHandler : MonoBehaviour
{
    //The robot prefab
    [SerializeField]
    public GameObject objectToPlace;

    //An indicator to show on the plane before putting down the robot 
    [SerializeField]
    public GameObject placementIndicator;

    //private object to keep track of placed object
    private GameObject placedObject;

    //
    private ARRaycastManager aRRayManager;
    private Pose PlacementPose;

    //state checker variables
    private bool placement = false;
    private bool placementPoseIsValid = false;
    private bool onTouchHold = false;
    private bool isRotating = false;

    private Vector2 touchPosition = default;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //vaiables to try and detect a double tap
    private int tapCount = 0;
    private float MaxDoubleTime = 0.3f;
    private float newTime = 0;

    private Vector3 rotationSpeed = new Vector3(0, 30, 0);

    // Start is called before the first frame update
    void Start()
    {
        aRRayManager = FindObjectOfType<ARRaycastManager>();
        placement = false;
        placementPoseIsValid = false;
        onTouchHold = false;
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        //methods to detect the orientation of the camera and then update the indicator
        updatePlacementPose();
        updatePlacementIndicator();
        //placing an object 
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && placement == false) { 
            placedObject = Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
            placementIndicator.SetActive(false);
            placement = true;
        } 
        else {//once an object has been placed, these methods implement dragging and dropping and double tapping 
            onHold();
            drag();
            doubleTapRotate();
            if (isRotating) {
                placedObject.transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);//should continously slowly rotate the object
                stopRotationTouch();
            }
        }
    }

    //method to try and see where the camera is pointing, both location and orientation
    private void updatePlacementPose() {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRayManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid) {
            PlacementPose = hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }
    }

    //method to try and update the indicator 
    private void updatePlacementIndicator() {
        if (placementPoseIsValid) {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else {
            placementIndicator.SetActive(false);
        }
    }

    //tries to detect when the object is being held
    private void onHold() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject)) {
                    if (ReferenceEquals(hitObject, placedObject)) {//not sure if the reference equals is the best way to compare the objects 
                        onTouchHold = true;
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended) {
                onTouchHold = false;
            }
        }
    }

    //updating the object location while its being held and dragged
    private void drag() {
        if (aRRayManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) {
            Pose hitPose = hits[0].pose;
            if (onTouchHold) {
                placedObject.transform.position = hitPose.position;
                placedObject.transform.rotation = hitPose.rotation;
            }
        }
    }

    private void doubleTapRotate() {
        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended) {
                tapCount += 1;
            }
            if (tapCount == 1) {
                newTime = Time.time + MaxDoubleTime;
            }
            else if (tapCount == 2 && Time.time <= newTime) {
                //need to use a raycasthit to see if touches were actually on robot
                isRotating = true;
                tapCount = 0;
            }
        }
        if (Time.time > newTime) {
            tapCount = 0;
        }
    }

    private void stopRotationTouch() {
        if(Input.touchCount > 0) {
            //check if touch was on robot
            isRotating = false;
        }
    }

}
