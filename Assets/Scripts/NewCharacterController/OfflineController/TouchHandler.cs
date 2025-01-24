using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 dragPos;
    public bool IsAiming = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        IsAiming = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsAiming = false;
    }
}
