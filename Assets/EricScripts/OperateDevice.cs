using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateDevice : MonoBehaviour
{
    public float radius = 1.5f;


    // Update is called once per frame
    void Update()
    {
        //GetKeyDown gets the keyboard key "C"
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Create sphere around player
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

            //^If collider near player, trigger "operate" if C key is down
            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 hitPosition = hitCollider.transform.position;
                hitPosition.y = transform.position.y;

                Vector3 direction = hitPosition - transform.position;

                //Use Dot to see if player is facing the wall
                if (Vector3.Dot(transform.forward, direction.normalized) > .5f)
                {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }

            }

        }

    }//end Update()
}