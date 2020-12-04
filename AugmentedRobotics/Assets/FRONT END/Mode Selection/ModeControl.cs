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

    //LM 26/11/20
    /*Android checking now also done by checking for ARCore. As we will only be implementing image
     * tracking then this solution is adaquate. Also IOS support can now be specified device by device
     * to give us additional control over the devices we support.
     */ 

    public bool isMobile, isARCapable, isARIdeal, isInVR, isInAR;   //false by default
    RuntimePlatform platform;

    int minAndroidSDK = 24;     //Minimum API version for ARCore support (Android 7) - corresponds to isARCapable
    int targetAndroidSDK = 25;  //If API is below this then only allow image tracked AR - corresponds to isARIdeal
    float minIOSVersion = 11f;  //Same here for IOS for ARKit
    float targetIOSVersion = 12f;   //Again modify this to allow only image tracking for below this version

    public GameObject modePanel;
    public GameObject arPanel;
    public GameObject vrButton, arButton, imageButton, planeButton;

    private const string ARCorePackageName = "com.google.ar.core";

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
        isInVR = false;
        isInAR = false;
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

            if (SDK >= minAndroidSDK && AndroidIsSupported())
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

            if (version >= minIOSVersion && IOSIsSupported())
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

        #if UNITY_EDITOR
        isMobile = true;
        isARCapable = true;
        isARIdeal = true;
        arButton.SetActive(true);      //Only allow VR if on desktop
        #endif
    }


    //SCENE LOADING

    public void LoadDesktopScene()
    {
        isInVR = true;
        isInAR = false;
        SceneManager.LoadScene("DesktopTest");
    }

    public void LoadVRScene()
    {
        isInVR = true;
        isInAR = false;
        SceneManager.LoadScene("DesktopTest");
    }

    public void LoadImageARScene()
    {
        isInVR = false;
        isInAR = true;
        SceneManager.LoadScene("ARMaster");
    }

    public void LoadFullARScene()
    {
        isInVR = false;
        isInAR = true;
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

    

    public static bool AndroidIsSupported()
    {
        var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        var packageManager = context.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject packageInfo = null;
        try
        {
            packageInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", ARCorePackageName, 0);
        }
        catch
        {

        }
        if (packageInfo != null)
            return true;
        else
            return false;
    }

    public static bool IOSIsSupported()
    {
        //Specify devices that are not supported based on ARKit docs, not suitable for even image tracking
        #if UNITY_EDITOR
        return true;
        #endif

        #if UNITY_IOS
        var gen = UnityEngine.iOS.Device.generation;
        Debug.Log("gen:" + gen);

        if (gen == UnityEngine.iOS.DeviceGeneration.iPhone4 ||
            gen == UnityEngine.iOS.DeviceGeneration.iPhone4S ||
            gen == UnityEngine.iOS.DeviceGeneration.iPhone5 ||
            gen == UnityEngine.iOS.DeviceGeneration.iPhone5C ||
            gen == UnityEngine.iOS.DeviceGeneration.iPhone5S ||
            gen == UnityEngine.iOS.DeviceGeneration.iPhone6 ||
            gen == UnityEngine.iOS.DeviceGeneration.iPhone6Plus ||
            gen == UnityEngine.iOS.DeviceGeneration.iPad1Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPad2Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPad3Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPad4Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPadAir1 ||
            gen == UnityEngine.iOS.DeviceGeneration.iPadAir2 ||
            gen == UnityEngine.iOS.DeviceGeneration.iPadMini1Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPadMini2Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPadMini3Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPadMini4Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPodTouch1Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPodTouch2Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPodTouch3Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPodTouch4Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPodTouch5Gen ||
            gen == UnityEngine.iOS.DeviceGeneration.iPodTouch6Gen)
        {
            Debug.Log("Device not supported");
            return false;
        }

        return true;
        #endif

        return false;
    }
}
