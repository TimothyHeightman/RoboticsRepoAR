using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    GameObject robot;
    Vector3 speed = new Vector3(0, 10, 0);
    // Start is called before the first frame update
    void Start()
    {
        robot = this.gameObject;
        //ArticulationBody art = robot.GetComponent<ArticulationBody>;
    }

    // Update is called once per frame
    void Update()
    {
        robot.transform.Rotate(Vector3.up, 10 * Time.deltaTime);
        
    }
}
