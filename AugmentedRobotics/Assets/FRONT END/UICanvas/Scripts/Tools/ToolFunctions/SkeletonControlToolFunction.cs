using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonControlToolFunction : Function
{

    [Header("References;")]
    [SerializeField] private GameObject skeletonOptionsPrefab;

    private GameObject skeletonOptions;

    private GameObject robotSelect;
    private GameObject robotSkeletonSelect;
    private GameObject skeletonSelect;

    void Awake()
    {
        // Instantiate prefab
        skeletonOptions = UIManager.Instance.InstantiatePrefab(skeletonOptionsPrefab, UIManager.Instance.openedTools.transform);
        skeletonOptions.SetActive(false);

        // Get references to the other selection buttons (only needed till skeleton bugs are fixed)
        robotSelect = skeletonOptions.transform.GetChild(0).gameObject;
        robotSkeletonSelect = skeletonOptions.transform.GetChild(1).gameObject;
        skeletonSelect = skeletonOptions.transform.GetChild(2).gameObject;

        // Default start with robot only as tool
        DeactivateProblemSequences("RobotTool");
    }

    void OnEnable()
    {
        // Activate options
        skeletonOptions.SetActive(true);

        // Deactivate modes that are currently creating bugs
        DeactivateProblemSequences(UIManager.Instance.activeSkeletonToolObject.name);
    }

    public override void OnDisable()
    {
        // Deactivate options
        skeletonOptions.SetActive(false);
    }

    void DeactivateProblemSequences(string thisToolName)
    {
        // Method to deactivate modes to ensure only possible sequence is followed
        // Only use this until we fix the skeleton bugs

        if (thisToolName == "RobotTool")
        {
            robotSelect.SetActive(true);
            robotSkeletonSelect.SetActive(true);
            skeletonSelect.SetActive(false);
        }
        else if (thisToolName == "SkeletonPlusRobotTool")
        {
            robotSelect.SetActive(false);
            robotSkeletonSelect.SetActive(true);
            skeletonSelect.SetActive(true);
        }
        else if (thisToolName == "SkeletonTool")
        {
            robotSelect.SetActive(false);
            robotSkeletonSelect.SetActive(false);
            skeletonSelect.SetActive(true);
        }
    }

}
