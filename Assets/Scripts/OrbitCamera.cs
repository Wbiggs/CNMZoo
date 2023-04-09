using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private float rotSpeed = 1.5f;
    [SerializeField] private float zoomSpeed = 0.05f;

    // [SerializeField] private float minDistance;
    // [SerializeField] private float maxDistance;
    private float rotY;
    private Vector3 offset;
    float zoomOffset = 1;

    // Start is called before the first frame update
    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = (target.position - transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /* float horInput = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(horInput, 0))
        {
            rotY += horInput * rotSpeed;
        }
        else
        { */
            rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        // }

        Quaternion rotation = Quaternion.Euler(0, rotY, 0);

        //only update the zoom offset if the scroll wheel is moving
        if(Input.mouseScrollDelta.magnitude > 0)
        {   
           zoomOffset -= Input.mouseScrollDelta.y * zoomSpeed;
        } 

        transform.position = (target.position) - (rotation * (offset * zoomOffset));
        transform.LookAt(target);
    }
}

