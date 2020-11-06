using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScaler : MonoBehaviour
{
    [SerializeField] float scale = 1.0f;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.localScale);
        var x = transform.localScale.x;
        var y = transform.localScale.y;
        var z = transform.localScale.z;

        transform.localScale = new Vector3(x, x, z);
    }
}
