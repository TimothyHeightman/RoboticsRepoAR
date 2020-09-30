using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticulationJointController : MonoBehaviour
{
    public float speed = 300.0f;

    private ArticulationBody articulation;
    private float userInput;

    void Start()
    {
        //Grab the ArticulationBody of this joint
        articulation = GetComponent<ArticulationBody>();
    }

    private void OnEnable()
    {
        userInput = 0f;
    }

    private void OnDisable()
    {
        StopRotation();
    }

    private void Update()
    {
        userInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {        
        if (userInput != 0)
        {
            float rotationChange = userInput * speed * Time.fixedDeltaTime;
            float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
            RotateTo(rotationGoal);
        }
    }

    // MOVEMENT HELPERS

    float CurrentPrimaryAxisRotation()
    {
        float currentRotationRads = articulation.jointPosition[0];
        float currentRotation = Mathf.Rad2Deg * currentRotationRads;
        return currentRotation;
    }

    private void RotateTo(float primaryAxisRotation)
    {
        //Takes in float primaryAxisRotation as a new angle to rotate to

        var drive = articulation.xDrive;
        drive.target = primaryAxisRotation;
        articulation.xDrive = drive;
    }

    public void StopRotation()
    {
        RotateTo(CurrentPrimaryAxisRotation()); //Stop motion upon ending manual movement
    }
}
