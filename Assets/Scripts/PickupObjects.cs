using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    public LayerMask layerMask;
    public float speed = 10f;
    public float distance;
    float actualDistance;
    public bool objectFront;
    bool objectSelected;
    
    
    RaycastHit hit;
    public GameObject selectedObject;
    private Rigidbody selectedRb;

    // Start is called before the first frame update
    void Start()
    {
        actualDistance = distance;
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
        PickupObject();
    }

    void Raycast()
    {
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            objectFront = true;
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
            objectFront = false;
            Debug.Log("Not Hit");
        }
    }
    void PickupObject()
    {
        if (objectFront)
        {
            if (Input.GetButton("Fire1"))
            {
                objectSelected = true;
                selectedObject = hit.transform.gameObject;
                selectedRb = selectedObject.GetComponent<Rigidbody>();
                
                selectedRb.isKinematic = true;
                selectedObject.transform.position = transform.position + transform.forward * distance;

            }
            if (Input.GetButtonUp("Fire1"))
            {
                
                selectedRb.isKinematic = false;
                selectedObject = null;
                selectedRb = null;
                objectSelected = false;
            }
        }
        if (!objectFront)
        {
            if (selectedRb != null)
            {
                selectedRb.isKinematic = false;
                selectedObject = null;
                selectedRb = null;
                objectSelected = false;
            }
        }
    }
    void ChangeObjectDistance()
    {
        if (objectSelected)
        {
            distance = actualDistance*Input.mouseScrollDelta.y;
        }
        else
        {
            distance = actualDistance;
        }
    }
}
