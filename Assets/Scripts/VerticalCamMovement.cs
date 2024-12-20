using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCamMovement : MonoBehaviour
{
    [Header("Senibilidad Y")]
    public float speed;
    float verticalRotation = 0f;

    void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * speed;
        verticalRotation -= vertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0); 
    }
}
