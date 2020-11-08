using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitTool : ButtonAnim, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData data)
    {
        Application.Quit();
    }

    public void OnClick()
    {
        Application.Quit();
    }

}