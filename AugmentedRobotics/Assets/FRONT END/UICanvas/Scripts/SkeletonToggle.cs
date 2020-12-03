using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkeletonToggle : MonoBehaviour
{
    private GameObject skeletonControlToolFunction;
    private Skeleton skeleton;
    private StandaloneSkeleton staSkel;
    private GameObject skelObj;


    void Start()
    {
        // Find skeleton and robot mesh references
        skeleton = UIManager.Instance.meshParent.GetComponentInChildren<Skeleton>();
        staSkel = UIManager.Instance.skeletonObject.GetComponent<StandaloneSkeleton>();
        skelObj = UIManager.Instance.skeletonObject;

        // Find the created gameObject
        Transform functionParent = UIManager.Instance.toolFunctionParent;
        skeletonControlToolFunction = functionParent.Find("SkeletonControlToolFunction").gameObject; 

        // Add listener to selection button
        this.GetComponent<Button>().onClick.AddListener( delegate{ ActivateSkeletonOption(gameObject.name); });
    }

    void OnEnable()
    {
        if (skeleton == null)
        {
            skeleton = GameObject.Find("RobotMeshes").GetComponentInChildren<Skeleton>();
            staSkel = GameObject.Find("Skeleton").GetComponent<StandaloneSkeleton>();
        }
    }

    void ActivateSkeletonOption(string thisButtonName)
    {

        // All options work the same
        // If button pressed is just robot
        if (thisButtonName == "JustRobot")
        {
            // If just robot not already active, activate just robot mode, and deactivate others
            
            if(UIManager.Instance.robotTool.activeSelf != true)
            {
                // Abdullah: do any hooking below here. This will enable the robot only mode
                if (skeleton.enabled) {
                    skeletonClearParams();
                    skeleton.enabled = false;
                }
                staSkel.switchToggle = true;
                UIManager.Instance.robotTool.SetActive(true);
                UIManager.Instance.robotPlusSkeletonTool.SetActive(false);
                UIManager.Instance.skeletonTool.SetActive(false);
            }

            // If just robot is already active, deactivate tool highlight
            UIManager.Instance.robotTool.transform.GetChild(0).gameObject.SetActive(false);
        }

        else if (thisButtonName == "RobotPlusSkeleton")
        {
            if(UIManager.Instance.robotPlusSkeletonTool.activeSelf != true)
            {
                // Abdullah: do any hooking below here. This will enable the robot + skeleton mode
                //skeleton.startFunc();
                skeleton.enabled = true;

                UIManager.Instance.robotTool.SetActive(false);
                UIManager.Instance.robotPlusSkeletonTool.SetActive(true);
                UIManager.Instance.skeletonTool.SetActive(false);
                UIManager.Instance.robotPlusSkeletonTool.transform.GetChild(0).gameObject.SetActive(true);
            }
            
            UIManager.Instance.robotPlusSkeletonTool.transform.GetChild(0).gameObject.SetActive(false);

        }

        else if (thisButtonName == "JustSkeleton")
        {
            if(UIManager.Instance.skeletonTool.activeSelf != true)
            {
                // Abdullah: do any hooking below here. This will enable the skeleton only mode
                skeletonClearParams();
                staSkel.switchToggle = true;

                UIManager.Instance.robotTool.SetActive(false);
                UIManager.Instance.robotPlusSkeletonTool.SetActive(false);
                UIManager.Instance.skeletonTool.SetActive(true);
                UIManager.Instance.skeletonTool.transform.GetChild(0).gameObject.SetActive(true); 
            }
            
            UIManager.Instance.skeletonTool.transform.GetChild(0).gameObject.SetActive(false);
        }

        else if (thisButtonName == "DeleteRobot")
        {
            // Destroy robot in scene - should we deactivate instead?
            Destroy(UIManager.Instance.meshParent.GetChild(0).gameObject);
            UIManager.Instance.isRobotInScene = false;

            // Deactivate skeleton - faster way to do this?
            skeletonClearParams();
                
            Destroy(skelObj);

            // Deactivate skeleton tool from tooltray
            UIManager.Instance.skeletonTool.SetActive(false);
            UIManager.Instance.skeletonTool.transform.GetChild(0).gameObject.SetActive(false);

            // Reactivate inventory tool
            UIManager.Instance.openedTools.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

        }

        // Deactivate selection pop up and SkeletonControlToolFunction
        transform.parent.gameObject.SetActive(false);
        skeletonControlToolFunction.SetActive(false);

    }


    void skeletonClearParams() {
        DeleteLineChildren(GameObject.Find("RobotMeshes"));
        skeleton.lines.Clear();
    }

    private void DeleteLineChildren(GameObject obj) {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform) {
            if (null == child)
                continue;
            if (child.CompareTag("Line") || child.CompareTag("Effects")) {//makes sure its a line renderer 
                DeleteLineChildren(child.gameObject);
                Destroy(child.gameObject);
            }
            else {
                DeleteLineChildren(child.gameObject);
            }
        }
    }
}
