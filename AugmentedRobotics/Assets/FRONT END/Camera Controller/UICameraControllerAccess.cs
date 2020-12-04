using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraControllerAccess : MonoBehaviour
{
    private Camera cam;
    private FlightController controller;

    private bool moving = false;

    private void Awake()
    {
        cam = Camera.main;
        controller = cam.GetComponent<FlightController>();
    }


   

    public void LEFT()
    {
        controller.CurrentRotation = "left";
        moving = true;
    }

    public void RIGHT()
    {
        controller.CurrentRotation = "right";
        moving = true;
    }

    public void FWD()
    {
        controller.CurrentTranslation = "fwd";
        moving = true;
    }

    public void BACK()
    {
        controller.CurrentTranslation = "back";

        moving = true;
    }



    private void Update()
    {

        if (moving)
        {
            //translation

            //if (Input.GetKey(KeyCode.W))
            //{
            //    controller.CurrentTranslation = "fwd";
            //}
            //else if (Input.GetKey(KeyCode.S))
            //{
            //    controller.CurrentTranslation = "back";
            //}


            //// rotation

            //if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A))
            //{
            //    controller.CurrentRotation = "left";
            //}
            //else if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.D))
            //{
            //    controller.CurrentRotation = "right";
            //}
        }
       

    }


    public void StopMotion()
    {
        ClearRotation();
        ClearTranslation();
        moving = false;
    }

    private void ClearRotation()
    {
        controller.CurrentRotation = null;
    }

    private void ClearTranslation()
    {
        controller.CurrentTranslation = null;
    }


}
