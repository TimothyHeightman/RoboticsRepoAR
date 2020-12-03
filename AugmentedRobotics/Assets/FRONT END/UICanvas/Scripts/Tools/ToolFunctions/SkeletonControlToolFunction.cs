using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonControlToolFunction : Function
{

    [Header("References;")]
    // This will be useful when all bugs between modes are gone
    // [SerializeField] private GameObject allSkeletonOptionsPrefab;

    // For now, use this list
    [SerializeField] private List<GameObject> skeletonOptionsPrefabList;

    private List<GameObject> skeletonOptions = new List<GameObject>();
    private List<GameObject> toolToSelect = new List<GameObject>();

    private int option;

    private GameObject optionSelect;

    void Awake()
    {
        PopulateOptionsList();
    }

    void OnEnable()
    {
        // Activate options
        SelectingRightOptionsWindow(UIManager.Instance.activeSkeletonToolObject.name);
        skeletonOptions[option].SetActive(true);
    }

    public override void OnDisable()
    {
        // Deactivate options
        skeletonOptions[option].SetActive(false);
    }

    private void PopulateOptionsList()
    {
        foreach(GameObject window in skeletonOptionsPrefabList)
        {
            GameObject skeletonOption = UIManager.Instance.InstantiatePrefab(window, UIManager.Instance.openedTools.transform);
            skeletonOptions.Add(skeletonOption);
            skeletonOption.SetActive(false);
        }
    }

    private void SelectingRightOptionsWindow(string currentToolInTooltray)
    {
        option = 0;

        if (currentToolInTooltray == "RobotTool")
        {
            option = 0;
        }
        else if (currentToolInTooltray == "SkeletonPlusRobotTool")
        {
           option = 1; 
        }
        else if (currentToolInTooltray == "SkeletonTool")
        {
            option = 2;
        }
    }
}
