using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToolFunction : Function
{
    //LM 01/10/20
    //Class that handles moving base of the articulated body, cannot be used for movement of joints

    public Robot selectedRobot;
    public Vector3 inputPos;

    public bool isTranslating, isRobotPresent;
    public Vector3 targetPos, newPos;
    [SerializeField] float smoothTime = 0.1f;        //controls snappiness of the translation
    [SerializeField] float maxSpeed = 20f;        //controls max speed of translation
    Vector3 baseVelocity = Vector3.zero;
    ArticulationBody baseBody;

    IEnumerator Translator()
    {
        newPos = selectedRobot.joints[0].transform.position;        

        while (targetPos != newPos)     //if we are not yet at the target location
        {
            newPos = Vector3.SmoothDamp(selectedRobot.joints[0].transform.position, targetPos, ref baseVelocity, smoothTime, maxSpeed);      //check our velocity works as expected
            baseBody.TeleportRoot(newPos, selectedRobot.joints[0].transform.rotation);
            Debug.Log("TRANSLATION ONGOING");
            yield return null;
        }

        isTranslating = false;
        Debug.Log("Coroutine Ending");
        gameObject.SetActive(false);
    }

    void UpdateReferences()
    {
        selectedRobot = SelectionManager.Instance.robot;
        isTranslating = false;
        targetPos = selectedRobot.joints[0].transform.position;
        baseBody = selectedRobot.joints[0].GetComponent<ArticulationBody>();
    }


    void OnEnable()
    {
        ProcessRefs();
    }
    private void Start()
    {
        ProcessRefs();
    }


    void ProcessRefs()
    {
        SelectionManager.Instance.moveToolFunction = this;
        if (SelectionManager.Instance.robot != null)
        {
            isRobotPresent = true;
            UpdateReferences();
        }
        else
        {
            isRobotPresent = false;
        }
    }

    private void Update()
    {
        if (isRobotPresent)
        {

            //Continously check if new input has been received and 
            CheckNewInput();
        }
    }


    public override void OnDisable()
    {
        //halt all movement on disabling of tool
        StopCoroutine("Translator");
        if (selectedRobot != null)
        {
            targetPos = selectedRobot.transform.position;
        }
    }

    
    void CheckNewInput()
    {
        //inputPos = Vector3.zero;  //Replace this with whatever method you need to obtain a target position from user input

        if (inputPos != targetPos)
        {
            Debug.Log("not equal");
            targetPos = inputPos;

            if (!isTranslating)
            {
                Debug.Log("Coroutine Starting");
                StartCoroutine("Translator");
                isTranslating = true;
            }
        }

    }
}