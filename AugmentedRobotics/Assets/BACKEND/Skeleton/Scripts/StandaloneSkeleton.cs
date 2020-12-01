using System.Collections.Generic;
using UnityEngine;

public class StandaloneSkeleton : MonoBehaviour
{
    public bool switchToggle = false;
    private bool currState = false;

    private GameObject frankaRobot;
    private Skeleton skeleton;
    private SkeletonDHGenerator skelDHGen;
    private SkelGizmoControl skelGizmos;
    private Robot robot;
    public List<Transform> joints;
    private GameObject joint;
    public List<GameObject> jointObjs = new List<GameObject>();
    public Material[] matteBlack = new Material[1];
    public Material[] matteRed = new Material[1];
    public Material[] metal = new Material[1];

    public GameObject sphere;


    private bool gizmoTog = false;

    // Start is called before the first frame update
    void Start()
    {
        frankaRobot = GameObject.Find("RobotMeshes");
        frankaRobot = frankaRobot.transform.Find("v4Complete").gameObject;
        robot = frankaRobot.GetComponent<Robot>();
        joints = robot.parts;
        skeleton = GetComponent<Skeleton>();
        skelDHGen = GetComponent<SkeletonDHGenerator>();
        skelGizmos = GetComponent<SkelGizmoControl>();
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
                skeletonClearParams();
                toSkeleton();
                currState = true;
            }
            switchToggle = false;
        }
        if (currState && skelDHGen.isActiveAndEnabled && (skelDHGen.dhParams.Length > 0)) {
            skelDHGen.GenerateAllParameters(joints);
            switchOnGizmos();
        }
        
    }

    public void toSkeleton() {
        disableMeshRenderer(frankaRobot);
        setSkeletonStructure();
        skelDHGen.enabled = true;
        sphereFunc();
    }

    public void toFranka() {
        skelDHGen.enabled = false;
        skeleton.enabled = false;
        skelGizmos.enabled = false;
        //DeleteJointChildren(this.gameObject);
        joints.Clear();
        frankaRobot.GetComponentInChildren<Skeleton>().enabled = false;
        enableMeshRenderer(frankaRobot);
        resetFrankaMat();
    }

    public void sphereFunc() {
        SphereGen spheres = gameObject.AddComponent<SphereGen>();
        spheres.instantiateSpheres(joints, sphere);
    }

    void setSkeletonStructure() {
        joint = new GameObject("Joint");
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

    private void enableMeshRenderer(GameObject obj) {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform) {
            if (null == child)
                continue;
            if (child.GetComponent<Renderer>()) {//to exclude empty objects, spheres and lines
                child.GetComponent<Renderer>().enabled = true;
            }
            enableMeshRenderer(child.gameObject);
        }
    }

    private void DeleteJointChildren(GameObject obj) {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform) {
            if (null == child) {
                continue;
            }
            DeleteJointChildren(child.gameObject);
            Destroy(child.gameObject);
        }
    }

    private void switchOnGizmos() {
        if (!gizmoTog) {
            skelGizmos.enabled = true;
        }
    }

    private void resetFrankaMat() {
        frankaRobot.transform.Find("Base").GetComponent<Renderer>().materials = matteBlack;
        frankaRobot.transform.Find("Base/Bone1").GetComponent<Renderer>().materials = matteBlack;
        frankaRobot.transform.Find("Base/Bone1/Bone2").GetComponent<Renderer>().materials = matteRed;
        frankaRobot.transform.Find("Base/Bone1/Bone2/Bone3").GetComponent<Renderer>().materials = matteBlack;
        frankaRobot.transform.Find("Base/Bone1/Bone2/Bone3/Bone4").GetComponent<Renderer>().materials = matteRed;
        frankaRobot.transform.Find("Base/Bone1/Bone2/Bone3/Bone4/Length1").GetComponent<Renderer>().materials = matteRed;
        frankaRobot.transform.Find("Base/Bone1/Bone2/Bone3/Bone4/Length1/Length2").GetComponent<Renderer>().materials = matteBlack;
        frankaRobot.transform.Find("Base/Bone1/Bone2/Bone3/Bone4/Length1/Length2/ClawLeft").GetComponent<Renderer>().materials = metal;
        frankaRobot.transform.Find("Base/Bone1/Bone2/Bone3/Bone4/Length1/Length2/ClawRight").GetComponent<Renderer>().materials = metal;
    }

    void skeletonClearParams() {
        DeleteLineChildren(GameObject.Find("RobotMeshes"));
        skeleton.spheres.Clear();
        skeleton.lines.Clear();
    }

    private void DeleteLineChildren(GameObject obj) {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform) {
            if (null == child)
                continue;
            if (child.CompareTag("Line") || child.CompareTag("Effects")) {//makes sure its a line renderer 
                DeleteLineChildren(child.gameObject);
                Destroy(child.gameObject);
            }
            else {
                DeleteLineChildren(child.gameObject);
            }
        }
    }

}
