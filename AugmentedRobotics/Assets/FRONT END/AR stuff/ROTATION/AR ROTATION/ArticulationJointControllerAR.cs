using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticulationJointControllerAR : MonoBehaviour
{
    public float speed = 0.5f;

    private ArticulationBody articulation;
    private float userInput;

    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotation;

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
        //userInput = Input.GetAxis("Horizontal");
        touch = Input.GetTouch(0);
    }

    private void FixedUpdate()
    {        
        //if (userInput != 0)
        //{
        //    float rotationChange = userInput * speed * Time.fixedDeltaTime;
        //    float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
        //    RotateTo(rotationGoal);
        //}

        if (touch.phase == TouchPhase.Moved)
        {
            Debug.Log("ROTATING");
            float rotationChange = -touch.deltaPosition.x * speed;
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
