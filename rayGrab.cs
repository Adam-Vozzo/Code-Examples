using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

//Raycast used for physical interaction in the world space
public class rayGrab : MonoBehaviour
{
    public Transform raycasterTransform;
    public void Hold()
    {
        // get the Transform component of the pointer
        Transform pointerTransform = raycasterTransform;

        // set the GameObject's parent to the pointer
        transform.SetParent(pointerTransform, false);

        // position it in the view
        transform.localPosition = new Vector3(0, 0, 2);

        // disable physics on the object while the player is holding it
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void Release()
    {
        // set the parent to the world
        transform.SetParent(null, true);

        // get the rigidbody physics component
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // reset velocity
        rigidbody.velocity = Vector3.zero;

        // enable physics
        rigidbody.isKinematic = false;
    }

    //Checking for collision with a grabbable object 
    private void OnCollisionEnter(Collision other)
    {
        //VR controller interaction triggers
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
            if (other.gameObject.CompareTag("Grabbable"))
            {
                Debug.Log("Selected");
                other.gameObject.transform.parent = gameObject.transform;
            }
        }
 
    }

}
