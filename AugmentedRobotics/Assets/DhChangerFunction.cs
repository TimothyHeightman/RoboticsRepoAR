using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DhChangerFunction : MonoBehaviour
{
    public int jointID = 1;
    public int paramID = 1;

    public float angleSpeed = 0.1f;
    public float linearSpeed = 0.01f;

    SkeletonInverseDH skeletonInverseDH;
    SkeletonDHGenerator skeletonDH;


    public GameObject jointDrop, paramDrop;

    private void Start()
    {
        jointID = 1;
        paramID = 0;
    }

    private void OnEnable()
    {
        skeletonInverseDH = UIManager.Instance.skeletonObject.GetComponent<SkeletonInverseDH>();
        skeletonDH = UIManager.Instance.skeletonObject.GetComponent<SkeletonDHGenerator>();
    }

    public void NewJointID(int newVal)
    {
        jointID = newVal+1;
    }
    public void NewParamID(int newVal)
    {
        paramID = newVal;
    }

    public void ChangeValue(bool isIncrease)
    {
        float valChange = 0f;

        //angles
        if (paramID == 1 || paramID == 3)
        {
            if (isIncrease)
            {
                valChange = angleSpeed;
            }
            else
            {
                valChange = -angleSpeed;
            }
            
            
        }
        else
        {
            if (isIncrease)
            {
                valChange = linearSpeed;
            }
            else
            {
                valChange = -linearSpeed;
            }            
        }

        skeletonInverseDH.ChangeParams(jointID, paramID, valChange);
        
    }

}
