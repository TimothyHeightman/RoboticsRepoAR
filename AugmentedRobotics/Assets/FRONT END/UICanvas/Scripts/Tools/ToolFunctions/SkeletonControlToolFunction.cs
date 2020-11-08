using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonControlToolFunction : Function
{

    [Header("References;")]
    [SerializeField] private GameObject skeletonOptionsPrefab;

    private GameObject skeletonOptions;

    void Awake()
    {
        skeletonOptions = UIManager.Instance.InstantiatePrefab(skeletonOptionsPrefab, UIManager.Instance.openedTools.transform);
        skeletonOptions.SetActive(false);
    }

    void OnEnable()
    {
        skeletonOptions.SetActive(true);
    }

    public override void OnDisable()
    {
        skeletonOptions.SetActive(false);
    }

}
