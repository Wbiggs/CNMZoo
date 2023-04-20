using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class OperatePenGates : MonoBehaviour
{
    [SerializeField] Vector3 dPos;
    private bool open;

    public void Operate()
    {
        if (open)
        {
            transform.position -= dPos;
        }
        else
        {
            transform.position += dPos;
        }

        open = !open;

    }


    public void Activate()
    {
        if (!open)
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
            open = true;
        }
    }
    public void Deactivate()
    {
        if (open)
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
            open = false;
        }
    }

}