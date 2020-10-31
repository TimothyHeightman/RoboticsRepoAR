using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScaler : MonoBehaviour
{
    [SerializeField] Vector3 scale = new Vector3(1, 1, 1);

    // Update is called once per frame
    void Update()
    {
        transform.localScale = scale;
    }
}
