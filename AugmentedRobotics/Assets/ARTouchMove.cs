using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveToolFunction))]
public class ARTouchMove : MonoBehaviour
{
    Touch touch;
    Camera ARCamera;
    MoveToolFunction moveTool;
    [SerializeField] float speedModifier;
    [SerializeField] float x_limit;
    [SerializeField] float z_limit;

    private void Start()
    {
        moveTool = GetComponent<MoveToolFunction>();
        ARCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 currentPosition = moveTool.newPos;
                Vector3 cameraPosition = ARCamera.transform.position;

                Vector3 deltaPosition = touch.deltaPosition;
                deltaPosition.y = 0;



                //float x_change;
                //float z_change;

                //Vector3 deltaPos = new Vector3(touch.deltaPosition.x * speedModifier, 0, touch.deltaPosition.y * speedModifier);
                //Vector3 deltaPos = new Vector3(x_change * speedModifier, 0, z_change * speedModifier);
                //Vector3 targetPos = moveTool.newPos + deltaPos;


                //moveTool.inputPos = targetPos;
            }
        }        
    }
}
