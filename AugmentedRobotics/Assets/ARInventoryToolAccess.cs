using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ARMoveToolAccess))]
public class ARInventoryToolAccess : MonoBehaviour
{
    public SelectionManager selectionManager;
    public InventoryToolFunction inventory;
    private ARMoveToolAccess moveTool;
    public GameObject Robot;
    //[Range(-1f,1f)][SerializeField] float x_pos;
    //[Range(-1f, 1f)][SerializeField] float z_pos;

    private void Awake()
    {
        moveTool = GetComponent<ARMoveToolAccess>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject temp;
            SpawnRobot(Vector3.zero, out temp);
        }            
    }

    public void SpawnRobot(Vector3 targetPos, out GameObject robotReference)
    {
        if (inventory == null)
        {
            inventory = selectionManager.InventoryToolFunction;
            
        }

        //if move tool not active, then activate
        if (!inventory.gameObject.activeSelf)
        {
            inventory.gameObject.SetActive(true);
        }

        inventory.SpawnRobot(out robotReference);

        if (robotReference)
        {
            Robot = robotReference;
            Debug.Log(Robot);
            inventory.gameObject.SetActive(false);
        }
    }





    IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }



}
