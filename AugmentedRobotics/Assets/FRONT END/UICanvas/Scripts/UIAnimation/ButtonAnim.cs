
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public virtual void OnPointerExit(PointerEventData data)
    {
        if (UIManager.Instance.activeTool != this.gameObject.GetComponent<Tool>())
        {
            highlight.SetActive(false);
        }
    }
}
