using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticulationJointController : MonoBehaviour
{
    public float speed = 300.0f;

    private ArticulationBody articulation;
    private float userInput;

    private Touch touch;
    private bool mobile;
    void Start()
    {
        //Grab the ArticulationBody of this joint
        articulation = GetComponent<ArticulationBody>();
        if (ModeControl.Instance != null)
        {
            mobile = ModeControl.Instance.isMobile;
        }
        else
        {
            mobile = false;
        }

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
#if (UNITY_EDITOR)
        userInput = Input.GetAxis("Horizontal");
        
#else
        if (mobile)
        {
            touch = Input.GetTouch(0);
        }
        else
        {
            userInput = Input.GetAxis("Horizontal");
        }
#endif
    }

    private void FixedUpdate()
    {
#if (!UNITY_EDITOR)
        if (mobile)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("ROTATING");
                float rotationChange = -touch.deltaPosition.x * speed;
                float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
                RotateTo(rotationGoal);
            }
        }
        else
        {
            if (userInput != 0)
            {
                float rotationChange = userInput * speed * Time.fixedDeltaTime * 2000;                
                float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
                RotateTo(rotationGoal);
            }            
        }
#else
        if (userInput != 0)
        {
            float rotationChange = userInput * speed * Time.fixedDeltaTime * 2000;
            float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
            RotateTo(rotationGoal);
        }
#endif

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
