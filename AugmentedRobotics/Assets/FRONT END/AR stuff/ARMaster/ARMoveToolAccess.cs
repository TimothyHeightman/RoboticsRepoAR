using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ARMoveToolAccess : MonoBehaviour
{
    public SelectionManager selectionManager;
    public MoveToolFunction moveTool;

    private Transform imageTransform;
    public Transform ImageTransform
    {           
        get { return imageTransform; }
        set { imageTransform = value; }
    }


    //[Range(-1f,1f)][SerializeField] float x_pos;
    //[Range(-1f, 1f)][SerializeField] float z_pos;

    [Range(-1f, 1f)] public float x_pos;
    [Range(-1f, 1f)] public float z_pos;
    [Range(-1f, 1f)] public float y_pos;

    public void MoveRobot(Vector3 targetPos, Quaternion targetRotation)
    {
        if(moveTool == null)
        {
            moveTool = selectionManager.moveToolFunction;
            
        }

        moveTool.gameObject.SetActive(true);

        moveTool.inputPos = targetPos;

        //targetRotation *= Quaternion.Euler(-90, 0, 0);

        moveTool.selectedRobot.transform.parent.SetPositionAndRotation(targetPos, targetRotation);

        Debug.Log("move tool newPos : " + moveTool.newPos);
        Debug.Log("move tool targetPos: " + moveTool.targetPos);
        Debug.Log("move tool inputPos: " + moveTool.inputPos);


    }

    void Update()
    {
        //Debug.Log(imageTransform.position);
        MoveRobot(imageTransform.position, imageTransform.rotation);
    }

    IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }



}