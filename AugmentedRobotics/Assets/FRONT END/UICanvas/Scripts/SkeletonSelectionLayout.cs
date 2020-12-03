using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSelectionLayout : MonoBehaviour
{
    private int activeSelectTools;
    private RectTransform rt;
    private RectTransform rtCurrentTool;
    private float toolInTooltrayXPosition;

    void Start()
    {
        rt = GetComponent<RectTransform>();

        rtCurrentTool = UIManager.Instance.activeSkeletonToolObject.GetComponent<RectTransform>();
        toolInTooltrayXPosition = rtCurrentTool.anchoredPosition.x;
        Debug.Log(toolInTooltrayXPosition);
    }

    void OnEnable()
    {
        foreach(Transform child in transform)
        {
            if(child.gameObject.activeSelf == true)
            {
                activeSelectTools += 1;
            }
        }
        Debug.Log(activeSelectTools);
        ResizeToNActiveSelectTools();
    }

    void ResizeToNActiveSelectTools()
    {
        switch (activeSelectTools)
        {
            case 0:
                Debug.LogError("No active tools! Check for bug.");
                break;

            case 1:
                rt.anchoredPosition = new Vector3 (rt.localPosition.x, 300f, 0);
                break;

            case 2:
                rt.anchoredPosition = new Vector3 (rt.localPosition.x, 420f, 0);
                break;

            case 3:
                rt.anchoredPosition = new Vector3 (rt.localPosition.x, 541.5f, 0);
                break;

        }

    }

    void OnDisable()
    {
        activeSelectTools = 0;
    }
}
