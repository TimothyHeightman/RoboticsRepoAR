using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    //gets robot and dHGenerator scripts from gameobject
    public Robot robot;
    public DHGenerator dHGenerator;

    //holds frames firat in dhparams then in transforms
    public Vector4[] dhparams;
    public List<Transform> joints;

    //sphere assigned to each joint
    public GameObject sphere;
    public List<GameObject> spheres;

    //material to make robot mesh translucent
    public Material[] newMaterialRef = new Material[1];

    //line renderers for d and a dh parameters
    public LineRenderer lineAX = new LineRenderer();
    public LineRenderer lineDZ = new LineRenderer();
    public List<LineRenderer> lines;


    // Start is called before the first frame update
    void Start()
    {
        //getting scripts and appropriate lists
        robot = GetComponent<Robot>();
        dHGenerator = GetComponent<DHGenerator>();
        dhparams = dHGenerator.dhParams;
        joints = robot.parts;

        //recursively change the material of every child of this gameobject to a translucent one
        GetChildMaterial(this.gameObject);

        //instantiate sphere at each frame
        foreach (Transform t in joints) {
            spheres.Add(Instantiate(sphere, t));
        }

        //Instantiate line renderers
        updateLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        //updates robot to stay translucent
        GetChildMaterial(this.gameObject);

        //updates line renderers to new frames
        updateLineRenderer();//possible optimisation is that this method is called upon a detected change in the app rather than on every update
    }

    //recursively finding each child of object and assigning material
    private void GetChildMaterial(GameObject obj) {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform) {
            if (null == child)
                continue;
            if (child.GetComponent<Renderer>() && (!child.CompareTag("Effects"))) {//to exclude empty objects, spheres and lines
                child.GetComponent<Renderer>().materials = newMaterialRef;
            }
            GetChildMaterial(child.gameObject);
        }
    }

    //recursively deletes duplicate line renderer objects 
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

        
    }

    //updates the line renderers for every frame
    private void updateLineRenderer() {
        DeleteLineChildren(this.gameObject);//deletes previous lines
        Vector3 obj1, obj2, dir, inter, temp;
        LineRenderer line;//placeholder line renderer object
        for(int i = 0; i < joints.Count - 1; i++) {
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

    //method for absolute value of every component of vector
    private Vector3 vectorAbs(Vector3 vec) {
        vec.x = Mathf.Abs(vec.x);
        vec.y = Mathf.Abs(vec.y);
        vec.z = Mathf.Abs(vec.z);

        return vec;
    }
}
