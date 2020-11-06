using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARInventoryToolAccess))]
[RequireComponent(typeof(ARMoveToolAccess))]
public class ImageTrackingObjectManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Image manager on the AR Session Origin")]
    ARTrackedImageManager m_ImageManager;
    ARMoveToolAccess moveTool;
    ARInventoryToolAccess inventory;
    SelectionManager selectionManager;

    private void Awake() 
    {
        inventory = GetComponent<ARInventoryToolAccess>();
        moveTool = GetComponent<ARMoveToolAccess>();
        selectionManager = FindObjectOfType<SelectionManager>();
    }
    /// <summary>
    /// Get the <c>ARTrackedImageManager</c>
    /// </summary>
    public ARTrackedImageManager ImageManager
    {
        get => m_ImageManager;
        set => m_ImageManager = value;
    }

    [SerializeField]
    [Tooltip("Reference Image Library")]
    XRReferenceImageLibrary m_ImageLibrary;

    /// <summary>
    /// Get the <c>XRReferenceImageLibrary</c>
    /// </summary>
    public XRReferenceImageLibrary ImageLibrary
    {
        get => m_ImageLibrary;
        set => m_ImageLibrary = value;
    }

    [SerializeField]
    [Tooltip("Prefab for tracked 1 image")]
    GameObject m_OnePrefab;

    /// <summary>
    /// Get the one prefab
    /// </summary>
    public GameObject onePrefab
    {
        get => m_OnePrefab;
        set => m_OnePrefab = value;
    }

    GameObject m_SpawnedOnePrefab;

    /// <summary>
    /// get the spawned one prefab
    /// </summary>
    public GameObject spawnedOnePrefab
    {
        get => m_SpawnedOnePrefab;
        set => m_SpawnedOnePrefab = value;
    }

    [SerializeField]
    [Tooltip("Prefab for tracked 2 image")]
    GameObject m_TwoPrefab;

    /// <summary>
    /// get the two prefab
    /// </summary>
    public GameObject twoPrefab
    {
        get => m_TwoPrefab;
        set => m_TwoPrefab = value;
    }

    GameObject m_SpawnedTwoPrefab;

    /// <summary>
    /// get the spawned two prefab
    /// </summary>
    public GameObject spawnedTwoPrefab
    {
        get => m_SpawnedTwoPrefab;
        set => m_SpawnedTwoPrefab = value;
    }

    int m_NumberOfTrackedImages;


    static Guid s_FirstImageGUID;
    static Guid s_SecondImageGUID;

    void OnEnable()
    {
        s_FirstImageGUID = m_ImageLibrary[0].guid;
        s_SecondImageGUID = m_ImageLibrary[1].guid;

        m_ImageManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
    }

    void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // added, spawn prefab
        foreach (ARTrackedImage image in obj.added)
        {
            if (image.referenceImage.guid == s_FirstImageGUID)
            {
                // m_SpawnedOnePrefab = Instantiate(m_OnePrefab, image.transform.position, image.transform.rotation);
                inventory.SpawnRobot(image.transform.position, out m_SpawnedOnePrefab);
                moveTool.ImageTransform = image.transform;
                SelectionManager.Instance.robot = m_SpawnedOnePrefab.GetComponentInChildren<Robot>();
            }
            else if (image.referenceImage.guid == s_SecondImageGUID)
            {
                m_SpawnedTwoPrefab = Instantiate(m_TwoPrefab, image.transform.position, image.transform.rotation);
                //SelectionManager.Instance.robot = m_SpawnedTwoPrefab.GetComponentInChildren<Robot>();
            }
        }

        // updated, set prefab position and rotation
        foreach (ARTrackedImage image in obj.updated)
        {
            // image is tracking or tracking with limited state, show visuals and update it's position and rotation
            if (image.trackingState == TrackingState.Tracking)
            {
                if (image.referenceImage.guid == s_FirstImageGUID)
                {
                    moveTool.MoveRobot(image.transform.position);
                    
                    
                    m_SpawnedOnePrefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    
                }
                else if (image.referenceImage.guid == s_SecondImageGUID)
                {
                    if (m_SpawnedTwoPrefab.CompareTag("Robot"))
                    {
                        moveTool.MoveRobot(image.transform.position);
                    }
                    else
                    {
                        m_SpawnedTwoPrefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    }
                }
            }
        }

        // removed, destroy spawned instance
        foreach (ARTrackedImage image in obj.removed)
        {
            if (image.referenceImage.guid == s_FirstImageGUID)
            {
                Destroy(m_SpawnedOnePrefab);
            }
            else if (image.referenceImage.guid == s_FirstImageGUID)
            {
                Destroy(m_SpawnedTwoPrefab);
            }
        }
    }

    public int NumberOfTrackedImages()
    {
        m_NumberOfTrackedImages = 0;
        foreach (ARTrackedImage image in m_ImageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                m_NumberOfTrackedImages++;
            }
        }
        return m_NumberOfTrackedImages;
    }
}
