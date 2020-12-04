using UnityEngine;

public class ForceLandscapeView : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }
}
