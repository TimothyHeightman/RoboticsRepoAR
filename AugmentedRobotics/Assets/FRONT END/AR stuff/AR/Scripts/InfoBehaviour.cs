using UnityEngine;

public class InfoBehaviour : MonoBehaviour
{
    const float SPEED = 6f;

    [SerializeField] Transform ObjectInfo;

    Vector3 desiredScale = Vector3.zero; //default size = 0

    // Update is called once per frame
    void Update()
    {
        ObjectInfo.localScale = Vector3.Lerp(ObjectInfo.localScale, desiredScale, Time.deltaTime * SPEED);
    }

    public void OpenInfo()
    {
        desiredScale = Vector3.one;
    }

    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
    }
}
