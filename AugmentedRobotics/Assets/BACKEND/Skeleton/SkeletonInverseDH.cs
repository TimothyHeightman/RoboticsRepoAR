using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonInverseDH : MonoBehaviour
{
    private SkeletonDHGenerator skelDHGen;
    private Skeleton skeleton;

    private int inputIndex = 1;


    private void Start()
    {
        skelDHGen = GetComponent<SkeletonDHGenerator>();
        skeleton = GetComponent<Skeleton>();
    }

    public void ChangeParams(int frameIndex, int valIndex, float newVal)
    {
        if (frameIndex < (skelDHGen.dhParams.Length + 1) && frameIndex > 0)
        {
            Vector4 currentParams = skelDHGen.dhParams[frameIndex - 1];
            Transform currentFrame = skelDHGen.tempFrames[frameIndex];
            Transform prevFrame = skelDHGen.tempFrames[frameIndex - 1];

            float valDiff = newVal - currentParams[valIndex];
            Vector3 movement = Vector3.zero; 

            switch (valIndex)
            {
                case 0: //a
                    movement = valDiff * (prevFrame.right.normalized);    //get vector to translate frame by
                    currentFrame.position += movement;
                    break;

                case 1: //alpha
                    currentFrame.transform.RotateAround(currentFrame.transform.position, prevFrame.right.normalized, valDiff);
                    break;

                case 2: //d
                    movement = valDiff * (prevFrame.forward.normalized);    //get vector to translate frame by
                    currentFrame.position += movement;
                    break;

                case 3: //theta
                    currentFrame.transform.RotateAround(currentFrame.transform.position, prevFrame.forward.normalized, valDiff);
                    break;

                default:
                    Debug.LogError("DH value index out of range");
                    break;
            }
        }
        if (!skeleton.enabled)
        {
            skeleton.enabled = true;
        }
        skelDHGen.GenerateAllParameters(SelectionManager.Instance.robot.parts);
        skeleton.setLineRenderers();
        
    }

    //PROTO DESKTOP INPUT MECHANISM

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        inputIndex += 1;
    //    }

    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        inputIndex -= 1;
    //    }

    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        float oldVal = skelDHGen.dhParams[inputIndex-1].x;
    //        float newVal = oldVal - 0.1f;
    //        ChangeParams(inputIndex, 0, newVal);
    //    }

    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        float oldVal = skelDHGen.dhParams[inputIndex-1].x;
    //        float newVal = oldVal + 0.1f;
    //        ChangeParams(inputIndex, 0, newVal);
    //    }

    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        float oldVal = skelDHGen.dhParams[inputIndex-1].z;
    //        float newVal = oldVal + 0.1f;
    //        ChangeParams(inputIndex, 2, newVal);
    //    }

    //    if (Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //        float oldVal = skelDHGen.dhParams[inputIndex-1].z;
    //        float newVal = oldVal - 0.1f;
    //        ChangeParams(inputIndex, 2, newVal);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        float oldVal = skelDHGen.dhParams[inputIndex - 1].y;
    //        float newVal = oldVal + 20f;
    //        ChangeParams(inputIndex, 1, newVal);
    //    }
    //}
}
