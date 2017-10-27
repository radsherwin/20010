using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    public UnityEvent mouseHover;
    public UnityEvent mouseExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseExit.Invoke();
    }
}
