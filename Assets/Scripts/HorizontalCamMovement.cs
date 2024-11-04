using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCamMovement : MonoBehaviour
{
    [Header("Senibilidad X")]
    public float speed;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * speed;

        Vector3 dir = new Vector3(0, horizontal, 0); 

        transform.Rotate(dir);
    }
}
