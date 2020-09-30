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
            marker.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Quaternion tempRot = new Quaternion();
            tempRot.eulerAngles = startToEndAngles;
            marker.transform.rotation = previousRotation * tempRot;
            //marker.transform.localRotation = articulationBody.anchorRotation;
        }
        
    }

    
}
