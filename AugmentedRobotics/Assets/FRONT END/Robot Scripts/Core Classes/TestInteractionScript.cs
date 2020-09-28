using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionScript : MonoBehaviour
{
    public Robot robot;
    public int jointIndex;
    public bool isMoving;

    void Start()
    {
        isMoving = true;
        jointIndex = 2;
        robot.ChangeRotationState(jointIndex, isMoving);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving;
            robot.ChangeRotationState(jointIndex, isMoving);
        }
    }
}
