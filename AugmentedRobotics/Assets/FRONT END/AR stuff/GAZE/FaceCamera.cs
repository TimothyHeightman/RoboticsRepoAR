using UnityEngine;


[ExecuteAlways]
public class FaceCamera : MonoBehaviour
{
    Transform cameraTransform;
    Vector3 targetAngle = Vector3.zero;

    void Start()
    {
        //fetch camera transform
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // rotate to face camera
        transform.LookAt(cameraTransform);

        //zero out rotation in x and z axes
        targetAngle = transform.localEulerAngles;
        targetAngle.x = 0;
        targetAngle.z = 0;
        targetAngle.y += 180;
        transform.localEulerAngles = targetAngle;
    }
}
