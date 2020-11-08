using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGizmo : MonoBehaviour
{
    float rotationsPerMinute = -25.0f;
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationsPerMinute * Time.deltaTime);
    }
}
