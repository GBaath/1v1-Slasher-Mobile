using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.Netcode;

public class NetworkedClickableObject : NetworkBehaviour, IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
{
    public UnityEvent press,release;
    public bool clickable = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickable)
        {

        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        press.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        release.Invoke();
    }

    public void SetClickable(bool clickable)
    {
        this.clickable = clickable;
    }
}
