using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneSkeleton : MonoBehaviour
{
    public bool switchToggle = false;
    private bool currState = false;

    private GameObject frankaRobot;
    private Skeleton skeleton;
    private SkeletonDHGenerator skelDHGen;
    private Robot robot;
    public List<Transform> joints;
    private GameObject joint;
    private List<GameObject> jointObjs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        joint = new GameObject("Joint");
        frankaRobot = GameObject.Find("RobotMeshes");
        frankaRobot = frankaRobot.transform.Find("v4Complete").gameObject;
        robot = frankaRobot.GetComponent<Robot>();
        joints = robot.parts;
        skeleton = GetComponent<Skeleton>();
        skelDHGen = GetComponent<SkeletonDHGenerator>();
        //need to attach dhgenerator to this, and generate a parent structured heirarchy for all the dh params
    }

    // Update is called once per frame
    void Update()
    {
        joints = robot.parts;
        if (switchToggle) {
            if (currState) {
                toFranka();
                currState = false;
            }
            else {
                toSkeleton();
                currState = true;
            }
            switchToggle = false;
        }
        if (currState && skelDHGen.isActiveAndEnabled && (skelDHGen.dhParams.Length > 0)) {
            skelDHGen.GenerateAllParameters(joints);
        }
        
    }

    void toSkeleton() {
        disableMeshRenderer(frankaRobot);
        setSkeletonStructure();
        skelDHGen.enabled = true;
        skeleton.enabled = true;
    }

    void toFranka() {
        skelDHGen.enabled = false;
        skeleton.enabled = false;
        this.gameObject.SetActive(false);
    }

    void setSkeletonStructure() {
        jointObjs.Add(Instantiate(joint, joints[0]));
        jointObjs[0].transform.parent = this.transform;
        joints[0] = jointObjs[0].transform;
        for(int i = 1; i < joints.Count; i++) {
            jointObjs.Add(Instantiate(joint, joints[i]));
            jointObjs[i].transform.parent = jointObjs[i-1].transform;
            joints[i] = jointObjs[i].transform;
        }
    }

    private void disableMeshRenderer(GameObject obj) {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform) {
            if (null == child)
                continue;
            if (child.GetComponent<Renderer>()) {//to exclude empty objects, spheres and lines
                child.GetComponent<Renderer>().enabled = false;
            }
            disableMeshRenderer(child.gameObject);
        }
    }

}
