using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public GameObject robotPart;
    public Vector3 startToEndAngles;    //CRUCIAL - defines the rotation from the previous frame to the current frame
    public bool isBase, isEnd;
    public ArticulationBody articulationBody;
    public ArticulationJointController jointController;
    public GameObject marker;
    public GameObject dhFrame;



    public void Setup()
    {
        robotPart = this.gameObject;
        articulationBody = this.GetComponent<ArticulationBody>();
    }



    public void PlaceMarker(Quaternion previousRotation)
    {
        marker.transform.localPosition = articulationBody.anchorPosition;
        if (isBase)
        {
            marker.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
        }
        else
        {
            Quaternion tempRot = new Quaternion();
            tempRot.eulerAngles = startToEndAngles;
            marker.transform.rotation = previousRotation * tempRot;
            //marker.transform.localRotation = articulationBody.anchorRotation;
        }        
    }

    public void PlaceDHFrame(GameObject jointMarker, GameObject previousFrame)
    {
        Vector3 displacement = jointMarker.transform.position - previousFrame.transform.position;
        Vector3 z1 = jointMarker.transform.forward;     //z axis direction in world space of frame to be moved
        float offset = Vector3.Dot(displacement, z1);
        jointMarker.transform.position -= z1 * offset;
    }

    
}
