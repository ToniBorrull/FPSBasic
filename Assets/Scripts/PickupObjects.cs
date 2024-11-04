using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    public LayerMask layerMask;
    public float distance;
    float actualDistance;
    public bool objectFront;
    bool objectSelected;
    public float maxDistance;
    public float minDistance;
    public float throwStrong;

    public GameObject followObj;

    private Vector3 lastPosition;
    private Vector3 currentPosition;
    private Vector3 objectVelocity;

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
        PickupObject();
        ChangeObjectDistance();
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
        if (Input.GetButtonDown("Fire1")){

            Raycast();
        
        }
        if (objectFront)
        {
            if (Input.GetButton("Fire1"))
            {
              
                objectSelected = true;
                selectedObject = hit.transform.gameObject;
                selectedRb = selectedObject.GetComponent<Rigidbody>();

                selectedRb.useGravity = false;
                followObj.transform.position = transform.position + transform.forward * distance;
                selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, followObj.transform.position, 3f * Time.deltaTime);
                
                //Calcular velocidad objetos
                lastPosition = currentPosition;

                currentPosition = selectedObject.transform.position;

                objectVelocity = (currentPosition - lastPosition) / Time.deltaTime;

                lastPosition = currentPosition;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                selectedRb.useGravity = true;

                selectedRb.velocity = objectVelocity * throwStrong;
                
                selectedObject = null;
                selectedRb = null;
                objectSelected = false;
                distance = actualDistance;
            }
        }
       /* if (!objectFront)
        {
            if (selectedRb != null)
            {
                selectedRb.useGravity = true;
                selectedObject = null;
                selectedRb = null;
                objectSelected = false;
     
            }
            distance = actualDistance;
        }*/
    }
    void ChangeObjectDistance()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
       
        if (scroll != 0f)
        {
            if (objectSelected)
            {
                distance += Input.mouseScrollDelta.y;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
            }
        }

    }
}
