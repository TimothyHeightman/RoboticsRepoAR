using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FlightController : MonoBehaviour {

    public Transform CentralTarget;
    public int ROTATE_SPEED = 10;

    private string currentRotation;
    public string CurrentRotation
    {
        get => currentRotation;
        set => currentRotation = value;
    }


    private string currentTranslation;
    public string CurrentTranslation
    {
        get => currentTranslation;
        set => currentTranslation = value;
    }


    [Tooltip("Enable/disable rotation control. For use in Unity editor only.")]
    public bool rotationEnabled = true;

    [Tooltip("Enable/disable translation control. For use in Unity editor only.")]
    public bool translationEnabled = true;

    [Tooltip("Mouse sensitivity")]
    public float mouseSensitivity = 1f;

    [Tooltip("Straffe Speed")]
    public float straffeSpeed = 5f;

    private float minimumX = -360f;
    private float maximumX = 360f;

    private float minimumY = -90f;
    private float maximumY = 90f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    Quaternion originalRotation;

    private Camera attachedCamera;
    private Vector3 axis;
    private Vector3 axisLastFrame;
    private Vector3 axisDelta;

    void Start()
    {
        originalRotation = transform.localRotation;
        attachedCamera = GetComponent<Camera>();
    }

    void Update() {
        if (translationEnabled)
        {
            float z = Time.deltaTime * straffeSpeed;
            if (currentTranslation == "fwd")
            {
                transform.Translate(0, 0, z);

            }
            if (currentTranslation == "back")
            {
                transform.Translate(0, 0, -z);
            }
        }

        if (rotationEnabled)
        {
            float theta = ROTATE_SPEED * Time.deltaTime;
            if (currentRotation == "left")
            {
                attachedCamera.transform.RotateAround(CentralTarget.position, Vector3.up, 8 * ROTATE_SPEED * Time.deltaTime); //for some reason rotate speed isnt changing the behaviour?
            }
            if (currentRotation == "right")
            {
                attachedCamera.transform.RotateAround(CentralTarget.position, Vector3.down, 8 * ROTATE_SPEED * Time.deltaTime);
            }
        }
    }

    void DisableEverything()
    {
        translationEnabled = false;
        rotationEnabled = false;
    }

    /// Enables rotation and translation control for desktop environments.
    /// For mobile environments, it enables rotation or translation according to
    /// the device capabilities.
    void EnableEverything()
    {
        rotationEnabled = translationEnabled = true;
    }

    public static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp (angle, min, max);
    }
}
