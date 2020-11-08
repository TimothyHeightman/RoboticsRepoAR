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
        foreach(MeshCard mesh in availableMeshes)
        {
            mesh.card.GetComponent<Button>().onClick.AddListener( delegate{ InstantiateRobot(mesh.id); });
        }
        inventoryToolHighlight = UIManager.Instance.useInAR.GetChild(2).GetChild(1).GetChild(1).GetChild(0).gameObject;
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

            // Instantiate DH Parameter table
            GameObject dhTable = UIManager.Instance.InstantiatePrefab(dhTablePrefab, UIManager.Instance.arUI);

            // Make sure it won't instantiate again
            activeMeshes.Add(item, newMesh);
            returnFromKey(availableMeshCardsDict, item).GetComponent<Button>().interactable = false;
            this.gameObject.SetActive(false);
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
