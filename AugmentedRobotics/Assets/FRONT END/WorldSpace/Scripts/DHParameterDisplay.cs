using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class DHParameterDisplay : MonoBehaviour
{
    private GameObject robot;
    private DHGenerator dHGenerator;
    private Vector4[] dHParamArray;

    [Header("DH Frames Textboxes")]
    [SerializeField] List<DHFrame> dhFrames = new List<DHFrame>();

    // Decide how many decimal places needed for student; might need to resize font in scene
    public static int decimalPlaces = 3;

    void Start()
    {
        robot = UIManager.Instance.meshParent.GetChild(0).gameObject;
        dHGenerator = robot.GetComponent<DHGenerator>();
        dHParamArray = dHGenerator.dhParams;
    }

    void Update()
    {
        for(int i = 0; i < dhFrames.Count-1; i++)
        {
            UpdateDHParameters(dhFrames[i+1], dHParamArray[i]);
        }
        
    }

    public static void UpdateDHParameters(DHFrame paramFrame, Vector4 updatedParam)
    {
        // Takes in a frame and the Vector4 of DH Parameters from the skeleton
        // and updates the TextMeshPro text

        List<TextMeshProUGUI> textList = paramFrame.orderedTextboxList;

        for(int i = 0; i<textList.Count; i++)
        {
            updatedParam[i] = Mathf.Round(updatedParam[i] * Mathf.Pow(10f, decimalPlaces)) / Mathf.Pow(10f, decimalPlaces);;
            textList[i].text = updatedParam[i].ToString();
        }

        Canvas.ForceUpdateCanvases();
    }
}
