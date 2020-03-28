using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollect : MonoBehaviour
{

    public GameObject PlayerRemote;
    public GameObject BridgeCollectEffect;

    //if collide with bridge part, update bridge objective in raycastselect.cs, destroy the part that they collided with.    
    //collision check
    void OnCollisionEnter(Collision col)
    {
        
        //if collides with bridge part
        if (col.gameObject.tag == "BridgePart")
        {
            Debug.Log("BridgePartCollect");
            ContactPoint contact = col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            Instantiate(BridgeCollectEffect, pos, rot); //Play particle effect

            PlayerRemote.GetComponent<raycastselect>().bridgeObjective++; //update number of bridge parts collected
        }
    }
}

//UI that tells the player to wait for the critters to collect the parts.
//another script that updates a UI for the player to see that they have the parts. 