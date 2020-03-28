using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placement of bridge parts to form a path
public class BridgeBuild : MonoBehaviour
{
    public GameObject player;
    public bool onBridge = false;
    private Collider box;

    //Should the object be affected by collision, forces and gravity
    private void FixedUpdate()
    {
        if(onBridge)
        {
            box.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        } else
        {
            box.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    //If bridge parts are placed in the correct area, update the player's progress
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Bridge Placed");

        if (col.gameObject.name == "BridgePart")
        {
            player.GetComponent<waypointMovement>().objective5++;
            
            onBridge = true;
            box = col;

        }
    }

    //If the player removes the part, update the player's progress negatively
    private void OnTriggerExit(Collider col)
    {
        Debug.Log("Bridge removed");

        if (col.gameObject.name == "BridgePart")
        {
            player.GetComponent<waypointMovement>().objective5--;
            onBridge = false;
            box = col;
        }
    }

}