using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum RobotMesh {
    Frank
}

[System.Serializable]
public class Mesh
{
    public RobotMesh id;
    public GameObject card;
    public GameObject prefab;


    public Mesh(RobotMesh id, GameObject card, GameObject prefab)
    {
        this.id = id;
        this.card = card;
        this.prefab = prefab;
    }
}


public class InventoryToolFunction : Function
{

    [Header("Robots: Assign Prefabs and Modal Cards to Enum")]
    [SerializeField] private List<Mesh> availableMeshes = new List<Mesh>();
    private Dictionary<RobotMesh, GameObject> availableMeshesDict = new Dictionary<RobotMesh, GameObject>();
    private Dictionary<RobotMesh, GameObject> availableMeshCardsDict = new Dictionary<RobotMesh, GameObject>();
    private Dictionary<RobotMesh, GameObject> activeMeshes = new Dictionary<RobotMesh, GameObject>();

    void Awake()
    {
        foreach(Mesh mesh in availableMeshes)
        {
            mesh.card.GetComponent<Button>().onClick.AddListener( delegate{ InstantiateRobot(mesh.id); });
        }
    }

    public void SpawnRobot(out GameObject robotReference)
    {
        robotReference = null;
        foreach (Mesh mesh in availableMeshes)
        {
            robotReference = InstantiateRobot(mesh.id);
        }
    }

    private void DictionaryMeshes()
    {
        // Assigns a key to each card to ease calling each object in program
        foreach(Mesh mesh in availableMeshes)
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
            GameObject meshPrefab = returnFromKey(availableMeshesDict, item);
            GameObject newMesh = UIManager.Instance.InstantiatePrefab(meshPrefab, UIManager.Instance.meshParent);
            activeMeshes.Add(item, newMesh);
            returnFromKey(availableMeshCardsDict, item).GetComponent<Button>().interactable = false;
            this.gameObject.SetActive(false);
            return newMesh;
        }
        return null;
    }

}
