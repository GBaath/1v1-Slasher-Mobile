using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour, IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
{
    public UnityEvent press,release,click;
    public bool clickable = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickable)
        {
            click.Invoke();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickable)
            press.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (clickable)
            release.Invoke();
    }

    public void SetClickable(bool clickable)
    {
        this.clickable = clickable;

        if (clickable)
            GetComponent<Image>().color = Color.white;
        else
            GetComponent<Image>().color = new Color(255, 255, 255, 100);
    }
}
