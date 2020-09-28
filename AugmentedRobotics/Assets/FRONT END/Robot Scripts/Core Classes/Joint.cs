using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LM 27/09/20
//Basic struct used to keep track of the joints in a robot, directly taken from demo

[System.Serializable]
public struct Joint
{
    public GameObject robotPart;
    public bool isBase;
    public ArticulationJointController jointController;
}