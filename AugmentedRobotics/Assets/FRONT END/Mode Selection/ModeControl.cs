using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeControl : MonoBehaviour
{
    //LM 07/11/20
    /*Class that handles the determination of the hardware currently in use. Based
     * on this, we modify a set of state variables to determine the modes of
     * operation that the user can choose between.
     */

    public bool isMobile, isARCapable, isARIdeal;   //false by default
    RuntimePlatform platform;

    int minAndroidSDK = 24;     //Minimum API version for ARCore support (Android 7) - corresponds to isARCapable
    int targetAndroidSDK = 25;  //If API is below this then only allow image tracked AR - corresponds to isARIdeal
    float minIOSVersion = 11f;  //Same here for IOS for ARKit
    float targetIOSVersion = 12f;   //Again modify this to allow only image tracking for below this version

    public GameObject modePanel;
    public GameObject arPanel;
    public GameObject vrButton, arButton, imageButton, planeButton;

    private static ModeControl _instance;

    public static ModeControl Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("ModeControl is NULL.");
            }

            return _instance;
        }
    }


    // Start is called before the first frame update
    private void Start()
    {        
        CheckDevice();
    }

    private void Awake()
    {
        _instance = this;
        Object.DontDestroyOnLoad(this);
    }


    void CheckDevice()
    {
        platform = Application.platform;


        if (platform == RuntimePlatform.Android)
        {
            isMobile = true;
            int SDK = getSDKInt();

            if (SDK >= minAndroidSDK)
            {
                isARCapable = true;     //Is device capable of ARCore?

                if (SDK >= targetAndroidSDK)
                {
                    isARIdeal = true;       //Is device capable of plane detection?                    
                }
                else
                {
                    planeButton.SetActive(false);
                }
            }
            else
            {
                arButton.SetActive(false);
            }
        }


        else if (platform == RuntimePlatform.IPhonePlayer)
        {
            isMobile = true;
            float version = iOSVersion;

            if (version >= minIOSVersion)
            {
                isARCapable = true;     //Is device capable of ARCore?

                if (version >= targetIOSVersion)
                {
                    isARIdeal = true;       //Is device capable of plane detection?
                }
                else
                {
                    planeButton.SetActive(false);
                }
            }
            else
            {
                planeButton.SetActive(false);
            }
        }


        else
        {
            isMobile = false;
            isARCapable = false;
            isARIdeal = false;
            arButton.SetActive(false);      //Only allow VR if on desktop
        }
    }


    //SCENE LOADING

    public void LoadDesktopScene()
    {
        SceneManager.LoadScene("DesktopTest");
    }

    public void LoadVRScene()
    {
        SceneManager.LoadScene("DesktopTest");
    }

    public void LoadImageARScene()
    {
        SceneManager.LoadScene("ARMaster");
    }

    public void LoadFullARScene()
    {
        SceneManager.LoadScene("ARMaster");
    }



    //VERSION DETECTION METHODS

    static int getSDKInt()
    {
        using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT");
        }
    }

    public static float iOSVersion
    {
        get
        {
            // SystemInfo.operatingSystem returns something like iPhone OS 6.1
            float osVersion = -1f;
            string versionString = SystemInfo.operatingSystem.Replace("iPhone OS ", "");
            float.TryParse(versionString.Substring(0, 1), out osVersion);

            return osVersion;
        }
    }
}
