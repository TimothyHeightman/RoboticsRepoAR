using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitTool : ButtonAnim
{
    public override void OnPointerClick(PointerEventData data)
    {
        Application.Quit();
    }

}