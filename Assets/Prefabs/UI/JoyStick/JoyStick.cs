using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform ThumbStickTrans;
    [SerializeField] RectTransform BackgroundTrans;
    [SerializeField] RectTransform CenterTrans;

    public delegate void OnStickInputValueUpdated(Vector2 inputVal);
    public delegate void OnStickTaped();

    public event OnStickInputValueUpdated onStickValueUpdated;
    public event OnStickTaped onStickTaped;

    bool bWasDragging;

    [SerializeField] private float smoothSpeed = 10f;

    private Vector2 currentThumbPosition;
    private Vector2 targetThumbPosition;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos = eventData.position;
        Vector2 centerPos = BackgroundTrans.position;

        Vector2 localOffset = Vector2.ClampMagnitude(touchPos - centerPos, BackgroundTrans.sizeDelta.x / 2);

        Vector2 inputVal = localOffset / (BackgroundTrans.sizeDelta.x / 2);

        targetThumbPosition = centerPos + localOffset;

        onStickValueUpdated?.Invoke(inputVal);
        bWasDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BackgroundTrans.position = eventData.position;
        ThumbStickTrans.position = eventData.position;
        currentThumbPosition = eventData.position;
        targetThumbPosition = eventData.position;
        bWasDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        BackgroundTrans.position = CenterTrans.position;
        ThumbStickTrans.position = BackgroundTrans.position;
        targetThumbPosition = BackgroundTrans.position;
        currentThumbPosition = BackgroundTrans.position;

        onStickValueUpdated?.Invoke(Vector2.zero);

        if (!bWasDragging)
        {
            onStickTaped?.Invoke();
        }
    }

    private void Update()
    {
        currentThumbPosition = Vector2.Lerp(currentThumbPosition, targetThumbPosition, Time.deltaTime * smoothSpeed);
        ThumbStickTrans.position = currentThumbPosition;
    }
}
