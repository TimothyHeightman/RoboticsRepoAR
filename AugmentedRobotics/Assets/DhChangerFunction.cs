using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DhChangerFunction : MonoBehaviour
{
    public int jointID = 1;
    public int paramID = 1;
    public float sliderval = 0.5f;

    bool trueChange = true;

    public GameObject jointDrop, paramDrop, valSlider;


    private void OnStart()
    {
        jointID = 1;
        paramID = 0;
        sliderval = 0.5f;
        bool trueChange = true;
    }
    public void NewJointID(int newVal)
    {
        jointID = newVal+1;
        valSlider.GetComponent<Slider>().value = 0.5f;
        trueChange = false;
    }
    public void NewParamID(int newVal)
    {
        paramID = newVal;
        valSlider.GetComponent<Slider>().value = 0.5f;
        trueChange = false;
    }

    public void PushChangeToVals()
    {
        SkeletonInverseDH skeletonInverseDH = UIManager.Instance.skeletonObject.GetComponent<SkeletonInverseDH>();
        sliderval = valSlider.GetComponent<Slider>().value;
        float valChange = 0f;

        //angles
        if (paramID == 1 || paramID == 3)
        {
            valChange = (sliderval - 0.5f)/0.5f * 180f;
        }
        else
        {
            valChange = (sliderval - 0.5f)/0.5f * 1f;
        }

        if (trueChange)
        {
            skeletonInverseDH.ChangeParams(jointID, paramID, valChange);
        }
        else
        {
            trueChange = true;
        }
        

        //work out what a change to the slider val corresponds to in terms of each specific param
    }

}
