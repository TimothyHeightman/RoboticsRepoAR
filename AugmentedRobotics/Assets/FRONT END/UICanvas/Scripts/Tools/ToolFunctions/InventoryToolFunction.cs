using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum RobotMesh {
    Frank
}

[System.Serializable]
public class MeshCard
{
    public RobotMesh id;
    public GameObject card;
    public GameObject prefab;


    public MeshCard(RobotMesh id, GameObject card, GameObject prefab)
    {
        this.id = id;
        this.card = card;
        this.prefab = prefab;
    }
}


public class InventoryToolFunction : Function
{

    [Header("Robots: Assign Prefabs and Modal Cards to Enum")]
    [SerializeField] private List<MeshCard> availableMeshes = new List<MeshCard>();
    private GameObject inventoryToolHighlight;
    private Dictionary<RobotMesh, GameObject> availableMeshesDict = new Dictionary<RobotMesh, GameObject>();
    private Dictionary<RobotMesh, GameObject> availableMeshCardsDict = new Dictionary<RobotMesh, GameObject>();
    private Dictionary<RobotMesh, GameObject> activeMeshes = new Dictionary<RobotMesh, GameObject>();

    [SerializeField] private GameObject dhTablePrefab;

    void Awake()
    {   
        // For each mesh card button in the inventory, add a listener to instantiate the mesh on click
        foreach(MeshCard mesh in availableMeshes)
        {
            mesh.card.GetComponent<Button>().onClick.AddListener( delegate{ InstantiateRobot(mesh.id); });
        }

        // Button feedback for click
        inventoryToolHighlight = UIManager.Instance.useInAR.gameObject;
        inventoryToolHighlight = UIManager.Instance.useInAR.GetChild(2).GetChild(1).GetChild(1).GetChild(0).gameObject;
    }

    void OnEnable()
    {
        // Temporary fix for reinstantiating robot after destroying. Will need to be written better
        // to generalise for multiple buttons
        if (UIManager.Instance.isRobotInScene == false && activeMeshes.Count != 0)
        {
            foreach(MeshCard mesh in availableMeshes)
            {
                activeMeshes.Remove(mesh.id);
                returnFromKey(availableMeshCardsDict, mesh.id).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void SpawnRobot(out GameObject robotReference)
    {
        robotReference = null;
        foreach (MeshCard mesh in availableMeshes)
        {
            robotReference = InstantiateRobot(mesh.id);
        }
    }

    private void DictionaryMeshes()
    {
        // Assigns a key to each card to ease calling each object in program
        foreach(MeshCard mesh in availableMeshes)
        {
            availableMeshesDict.Add(mesh.id, mesh.prefab);
            availableMeshCardsDict.Add(mesh.id, mesh.card);
        }
    }
    private GameObject returnFromKey(Dictionary<RobotMesh, GameObject> dictOfCardsOrPrefabs, RobotMesh thisItem)
    {
        if (dictOfCardsOrPrefabs.ContainsKey(thisItem) == false)
        {
            DictionaryMeshes();
        }

        return dictOfCardsOrPrefabs[thisItem];
    }

    public GameObject ReturnTrueIfMeshActive(RobotMesh item)
    {
        if (activeMeshes.ContainsKey(item) != false)
        {
            return availableMeshesDict[item];
        }
        else
        {
            return null;
        }
    }

    private GameObject InstantiateRobot(RobotMesh item)
    {
        if (activeMeshes.ContainsKey(item) == false)
        {
            // Instantiate mesh
            GameObject meshPrefab = returnFromKey(availableMeshesDict, item);
            GameObject newMesh = UIManager.Instance.InstantiatePrefab(meshPrefab, UIManager.Instance.meshParent);
            UIManager.Instance.robotTool.SetActive(true);
            // Instantiate DH Parameter table
            GameObject dhTable = UIManager.Instance.InstantiatePrefab(dhTablePrefab, UIManager.Instance.arUI);
            UIManager.Instance.dhTable = dhTable;

            // Make sure it won't instantiate again
            activeMeshes.Add(item, newMesh);
            returnFromKey(availableMeshCardsDict, item).GetComponent<Button>().interactable = false;

            // Tell UIManager that robot is in scene, activate the robot tool for skeleton selection
            UIManager.Instance.isRobotInScene = true;
            UIManager.Instance.robotTool.SetActive(true);
            UIManager.Instance.activeSkeletonToolObject = UIManager.Instance.robotTool;

            // Instantiate skeleton
            UIManager.Instance.skeletonObject = Instantiate(UIManager.Instance.skeletonObjectPrefab);
            UIManager.Instance.skeletonObject.name = UIManager.Instance.skeletonObjectPrefab.name;

            // Deactivate inventory and inventory tool
            gameObject.SetActive(false);
            //UIManager.Instance.openedTools.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            if (ModeControl.Instance.isInAR)
            {
                UIManager.Instance.inventoryButton.SetActive(false);
            }            

            return newMesh;
        }
        return null;
    }
    public override void OnDisable()
    {
        UIManager.Instance.activeTool = null;
        if ( inventoryToolHighlight.activeSelf == true)
        {
            inventoryToolHighlight.SetActive(false);
        }
    }

}
