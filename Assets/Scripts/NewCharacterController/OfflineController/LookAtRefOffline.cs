using UnityEngine;

public class LookAtRefOffline : MonoBehaviour
{
    public GameObject LookAtObject;

    void FixedUpdate()
    {
        this.transform.position = LookAtObject.transform.position;
    }
}
