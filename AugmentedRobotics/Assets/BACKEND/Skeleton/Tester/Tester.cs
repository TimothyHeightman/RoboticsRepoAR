using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public LineRenderer line;
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 z = sphere1.transform.forward;
        //Debug.Log(z);


        /*
        line = Instantiate(line);
        Vector3 z = sphere1.transform.forward;
        vectorAbs(z);
        //Debug.Log(z);
        line.SetPosition(0, sphere1.transform.position);
        //Debug.Log(sphere1.transform.position);
        Vector3 rand = sphere3.transform.position - sphere1.transform.position;
        vectorAbs(rand);
        Vector3 inter = sphere1.transform.position + Vector3.Scale(z, rand);
        line.SetPosition(1, inter);
        //Debug.Log(Vector3.Scale(z, sphere3.transform.position));

        Vector3 x = sphere1.transform.right;
        //vectorAbs(x);

        Debug.Log(sphere1.transform.right);
        line = Instantiate(line);
        line.SetPosition(0, inter);
        rand = (sphere3.transform.position - inter);
        Debug.Log(rand);
        rand = vectorAbs(rand);
        Debug.Log(rand);
        line.SetPosition(1, inter + Vector3.Scale(x, rand));
        Debug.Log(inter);
        Debug.Log(Vector3.Scale(x, rand));
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        Debug.Log("ok");
    }

    private Vector3 vectorAbs(Vector3 vec) {
        vec.x = Mathf.Abs(vec.x);
        vec.y = Mathf.Abs(vec.y);
        vec.z = Mathf.Abs(vec.z);

        return vec;
    }
}
