
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool isToolStillActiveAfterClick;

    GameObject highlight;

    void Awake()
    {
        highlight = this.transform.GetChild(0).gameObject;      //THIS KEEPS THROWING AN ERROR
    }

    public virtual void OnPointerEnter(PointerEventData data)
    {
        highlight.SetActive(true);

    }

    public virtual void OnPointerClick(PointerEventData data)
    {
        Application.Quit();
    }

    public virtual void OnPointerExit(PointerEventData data)
    {
        highlight.SetActive(false);
    }
}
