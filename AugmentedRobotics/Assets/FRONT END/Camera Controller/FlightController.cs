using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FlightController : MonoBehaviour {

    public Transform CentralTarget;
    public int ROTATE_SPEED = 10;
    


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
            //float x = Input.GetAxis("Horizontal") * Time.deltaTime * straffeSpeed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * straffeSpeed;

            transform.Translate(0, 0, z);
        }

        if (rotationEnabled)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    axisLastFrame = attachedCamera.ScreenToViewportPoint(Input.mousePosition);
            //}
            //if (Input.GetMouseButton(0))
            //{
            //    axis = attachedCamera.ScreenToViewportPoint(Input.mousePosition);
            //    axisDelta = (axisLastFrame - axis) * 90f;
            //    axisLastFrame = axis;

            //    rotationX -= axisDelta.x * mouseSensitivity;
            //    rotationY -= axisDelta.y * mouseSensitivity;

            //    rotationX = ClampAngle (rotationX, minimumX, maximumX);
            //    rotationY = ClampAngle (rotationY, minimumY, maximumY);

            //    Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
            //    Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);

            //    transform.localRotation = originalRotation * xQuaternion * yQuaternion;
            //}
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A))
            {
                attachedCamera.transform.RotateAround(CentralTarget.position, Vector3.up, ROTATE_SPEED * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.D))
            {
                attachedCamera.transform.RotateAround(CentralTarget.position, Vector3.down, ROTATE_SPEED * Time.deltaTime);
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
