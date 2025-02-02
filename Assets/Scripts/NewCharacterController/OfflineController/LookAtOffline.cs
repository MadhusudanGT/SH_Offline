using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LookAtOffline : MonoBehaviour
{
    private Vector3 worldPosition;
    private Vector3 screenPosition;
    public GameObject crosshair;
    private void Start()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = 3f;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;
        crosshair.transform.position = Input.mousePosition;
    }
}