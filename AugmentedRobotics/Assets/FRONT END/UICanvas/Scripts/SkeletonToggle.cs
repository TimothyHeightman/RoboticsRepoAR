using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkeletonToggle : MonoBehaviour
{
    private GameObject skeletonControlToolFunction;
    void Start()
    {
        Transform functionParent = UIManager.Instance.toolFunctionParent;
        skeletonControlToolFunction = functionParent.Find("SkeletonControlToolFunction").gameObject;   
        // Add listener to selection button
        this.GetComponent<Button>().onClick.AddListener( delegate{ ActivateSkeletonOption(gameObject.name); });
    }

    void ActivateSkeletonOption(string thisButtonName)
    {

        // All options work the same
        // If button pressed is just robot
        if (thisButtonName == "JustRobot")
        {
            // If just robot not already active, activate just robot mode, and deactivate others
            if(UIManager.Instance.justRobotTool.activeSelf != true)
            {
                // If just skeleton mode is active, respawn robot
                /*if (UIManager.Instance.justSkeletonTool.activeSelf == true)
                {
                    RespawnRobot();
                }*/

                // Abdullah: do any hooking below here. This will enable the robot only mode


                UIManager.Instance.justRobotTool.SetActive(true);
                UIManager.Instance.robotPlusSkeletonTool.SetActive(false);
                UIManager.Instance.justSkeletonTool.SetActive(false);
            }

            // If just robot is already active, deactivate tool highlight
            UIManager.Instance.justRobotTool.transform.GetChild(0).gameObject.SetActive(false);
        }

        else if (thisButtonName == "RobotPlusSkeleton")
        {
            if(UIManager.Instance.robotPlusSkeletonTool.activeSelf != true)
            {   
                // If just skeleton mode is active, respawn robot
                /*if (UIManager.Instance.justSkeletonTool.activeSelf == true)
                {
                    RespawnRobot();
                }*/

                // Abdullah: do any hooking below here. This will enable the robot + skeleton mode


                UIManager.Instance.justRobotTool.SetActive(false);
                UIManager.Instance.robotPlusSkeletonTool.SetActive(true);
                UIManager.Instance.justSkeletonTool.SetActive(false);
                UIManager.Instance.robotPlusSkeletonTool.transform.GetChild(0).gameObject.SetActive(true);
            }
            
            UIManager.Instance.robotPlusSkeletonTool.transform.GetChild(0).gameObject.SetActive(false);
        }

        else if (thisButtonName == "JustSkeleton")
        {
            if(UIManager.Instance.justSkeletonTool.activeSelf != true)
            {
                // Abdullah: do any hooking below here. This will enable the skeleton only mode


                UIManager.Instance.justRobotTool.SetActive(false);
                UIManager.Instance.robotPlusSkeletonTool.SetActive(false);
                UIManager.Instance.justSkeletonTool.SetActive(true);
                UIManager.Instance.justSkeletonTool.transform.GetChild(0).gameObject.SetActive(true); 
            }
            
            UIManager.Instance.justSkeletonTool.transform.GetChild(0).gameObject.SetActive(false);
        }

        // Deactivate selection pop up and SkeletonControlToolFunction
        transform.parent.gameObject.SetActive(false);
        skeletonControlToolFunction.SetActive(false);

    }

    /*void RespawnRobot()
    {

    }*/
}
