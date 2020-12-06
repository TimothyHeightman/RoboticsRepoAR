using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class basicButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //LM 06/12/20 - Class to handle functionality of dh parameter changing tools

    public bool isIncrease;
    public bool isPressed = false;
    public DhChangerFunction changerFunction;

    IEnumerator Changer()
    {
        while (isPressed)
        {
            changerFunction.ChangeValue(isIncrease);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        StartCoroutine("Changer");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
