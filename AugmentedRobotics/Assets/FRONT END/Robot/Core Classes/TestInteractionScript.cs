using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionScript : MonoBehaviour
{
    public Robot robot;
    public int selectedJointIndex;
    public bool isMoving;

    void Start()
    {
        isMoving = true;
        selectedJointIndex = 1;
        robot.ChangeRotationState(selectedJointIndex, isMoving);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving;
            robot.ChangeRotationState(selectedJointIndex, isMoving);        //CALL THIS METHOD TO TOGGLE MOVEMENT
        }
    }

}
