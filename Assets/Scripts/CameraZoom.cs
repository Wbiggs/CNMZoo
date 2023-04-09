using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position;
        pos.z += Input.mouseScrollDelta.y * speed;
        target.position = pos;
    }
}
