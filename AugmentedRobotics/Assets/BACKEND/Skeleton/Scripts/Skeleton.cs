﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    //gets robot and dHGenerator scripts from gameobject
    private DHGenerator dHGenerator;
    private SkeletonDHGenerator skelDHGen;
    private GameObject toolFunc;
    private GameObject rotateTool;

    //holds frames firat in dhparams then in transforms
    private Vector4[] dhparams;
    private List<Transform> joints;

    //sphere assigned to each joint
    public GameObject sphere;
    public List<GameObject> spheres;

    //material to make robot mesh translucent
    public Material[] newMaterialRef = new Material[2];

    //line renderers for d and a dh parameters
    public LineRenderer lineAX = new LineRenderer();
    public LineRenderer lineDZ = new LineRenderer();
    public List<LineRenderer> lines;

    private GameObject skeleton;

    // Start is called before the first frame update
 
    private void OnEnable() {
        startFunc();
        sphereFunc();
    }

    // Update is called once per frame
    void Update()
    {
        //updates robot to stay translucent
        if (GetComponent<DHGenerator>() && (!toolFunc.GetComponentInChildren<RotateToolFunction>())) {
            GetChildMaterial(this.gameObject);
        }
        //updates line renderers to new frames
        setLineRenderers();//possible optimisation is that this method is called upon a detected change in the app rather than on every update
    }

    public void startFunc() {
        if (GetComponent<DHGenerator>()) {
            dHGenerator = GetComponent<DHGenerator>();
            dHGenerator.robot.UpdatePartsTransforms();
            dHGenerator.tempFrames = dHGenerator.robot.parts;
            dhparams = dHGenerator.dhParams;
            joints = dHGenerator.tempFrames;
            skeleton = GameObject.Find("Skeleton");
            skeleton.GetComponent<StandaloneSkeleton>().enabled = true;
            toolFunc = GameObject.Find("ToolsFunctions");

        }
        else {
            skelDHGen = GetComponent<SkeletonDHGenerator>();
            dhparams = skelDHGen.dhParams;
            joints = skelDHGen.tempFrames;
        }

        //recursively change the material of every child of this gameobject to a translucent one
        if (GetComponent<DHGenerator>()) {
            GetChildMaterial(this.gameObject);
        }


        //Instantiate line renderers
        setLineRenderers();
    }

    public void sphereFunc() {
        SphereGen spheres = gameObject.AddComponent<SphereGen>();
        spheres.instantiateSpheres(joints, sphere);
    }

    //recursively finding each child of object and assigning material
    private void GetChildMaterial(GameObject obj) {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform) {
            if (null == child)
                continue;
            if (child.GetComponent<Renderer>() && (!child.CompareTag("Effects")) && (!child.CompareTag("Line")) && (!child.CompareTag("hasInfo"))) {//to exclude empty objects, spheres and lines
                child.GetComponent<Renderer>().materials = newMaterialRef;
            }
            GetChildMaterial(child.gameObject);
        }
    }

    //recursively deletes duplicate line renderer objects, need to empty array as well, without null pointers
    private void DeleteLineChildren(GameObject obj) {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform) {
            if (null == child)
                continue;
            if (child.CompareTag("Line")) {//makes sure its a line renderer 
                DeleteLineChildren(child.gameObject);
                Destroy(child.gameObject);
            }
            else {
                DeleteLineChildren(child.gameObject);
            }
        }
        lines.Clear();
        
    }

    public void setLineRenderers() {
        DeleteLineChildren(this.gameObject);//deletes previous lines, need to empty array as well
        Vector3 obj1, obj2, dir, inter, temp;
        LineRenderer line;//placeholder line renderer object
        for (int i = 0; i < joints.Count - 1; i++) {
            //Vector3 static class dot product 
            obj1 = joints[i].position;//current frame
            obj2 = joints[i + 1].position;//next frame


            //starting with line for d parameter, in z axis
            line = Instantiate(lineDZ);
            line.transform.parent = joints[i];//makes line child of current dh frame
            dir = joints[i].forward;//direction of z axis
            line.SetPosition(0, obj1);//line initial
            temp = dir * dhparams[i].z;//gets z dhparam 
            inter = obj1 + temp;
            line.SetPosition(1, inter);//sets end of line
            lines.Add(line);//added to list

            //line for a param, in x axis
            line = Instantiate(lineAX);
            line.transform.parent = joints[i];
            dir = joints[i].right;//direction of x axis

            line.SetPosition(0, inter);//setting line pos
            temp = dir * dhparams[i].x;
            line.SetPosition(1, inter + temp);
            lines.Add(line);
        }
    }

    //updates the line renderers for every frame
    private void updateLineRenderer() {
        int counter = 0;
        Vector3 obj1, obj2, dir, inter, temp;
        for(int i = 0; i < joints.Count - 1; i++) {
            //Vector3 static class dot product 
            obj1 = joints[i].position;//current frame
            obj2 = joints[i + 1].position;//next frame


            //starting with line for d parameter, in z axis

            dir = lines[counter].transform.parent.forward;//direction of z axis

            lines[counter].SetPosition(0, obj1);//line initial
            temp = dir * dhparams[i].z;//gets z dhparam 
            inter = obj1 + temp;
            lines[counter].SetPosition(1, inter);//sets end of line

            

            //line for a param, in x axis

            dir = lines[counter].transform.parent.right;//direction of x axis

            lines[counter+1].SetPosition(0, inter);//setting line pos
            temp = dir * dhparams[i].x;
            lines[counter+1].SetPosition(1, inter + temp);
            counter += 2;
        }
    }

    //method for absolute value of every component of vector
    private Vector3 vectorAbs(Vector3 vec) {
        vec.x = Mathf.Abs(vec.x);
        vec.y = Mathf.Abs(vec.y);
        vec.z = Mathf.Abs(vec.z);

        return vec;
    }
}
