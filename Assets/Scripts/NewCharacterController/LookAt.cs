using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class LookAt : MonoBehaviour
{
    private Vector3 worldPosition;
    private Vector3 screenPosition;
    public GameObject crosshair;
    public TouchHandler touchHandler;
    public float speed;
    public MobileInputController joystickController;
    public float screenOffset = 50f;
    void FixedUpdate()
    {
        float deltaX = joystickController.Horizontal * speed * Time.deltaTime; // Multiply by speed and Time.deltaTime
        float deltaY = joystickController.Vertical * speed * Time.deltaTime;
        //Debug.Log($"Joystick Input - Horizontal: {deltaX}, Vertical: {deltaY}");
        Vector3 newPosition = crosshair.transform.position + new Vector3(deltaX, deltaY, 0);

        newPosition.z = 10f;

        worldPosition = Camera.main.ScreenToWorldPoint(newPosition);


        screenPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width - screenOffset);
        screenPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height - screenOffset);

        crosshair.transform.position = screenPosition;
        transform.position = worldPosition;
    }
}
