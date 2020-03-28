using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ControllerSelection;


public class raycastselect : MonoBehaviour
{
    public int bridgeObjective = 0;
    public int BridgeCollection;

    public GameObject raycastObject;
    public UnityEvent bridgeBuild1;
    public UnityEvent bridgeBuild2;
    public UnityEvent bridgeBuild3;
    public UnityEvent bridgeBuild4;
    public UnityEvent bridgeBuild5;
    public LineRenderer linePointer = null;
    public float rayDrawDistance = 500;
    public GameObject player;
    public Material OldMat;


    public void Update()
    {
        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
        Ray ray = new Ray(raycastObject.transform.position, fwd); //controller raycast

        //Draw raycast visually
        linePointer.SetPosition(0, ray.origin);
        linePointer.SetPosition(1, ray.origin + ray.direction * rayDrawDistance);

        Debug.DrawRay(raycastObject.transform.position, fwd * 50, Color.green, 1, true);

        RaycastHit hit;

        //Enable the ray if the trigger is pressed.
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad)) //|| Input.GetMouseButton(0) //for testing without VR headset on
        {
            
            linePointer.enabled = true;

            //hit.transform.GetComponent<Renderer>().material.name //gets the name of the material of the object the ray hit (very cool, may be useful for checking which object has been hit)
            //hit.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterial //or this depending on the object hit

            //If the user is attempting to construct the bridge, make sure they have collected the bridge pieces first by checking the objectives.
            //Ensure the bridge is constructed in the correct order
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.tag == "BridgeSpot1" && BridgeCollection == 5 && bridgeObjective == 0) // && hit.transform.GetComponent<Renderer>().material.name == "Bridge_Placeholder (Instance) && BridgeCollection == 5 (all bridge pieces collected)
            {
                Debug.Log("bridgecast hit");

                bridgeBuild1.Invoke(); //runs the unity event for the selected object
                bridgeObjective++; //make sure these only invoke once [bridgeObjective = 0], in order

            }
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.tag == "BridgeSpot2" && BridgeCollection == 5 && bridgeObjective == 1) //  && BridgeCollection == 5 (all bridge pieces collected)
            {
                Debug.Log("bridgecast hit");

                bridgeBuild2.Invoke(); //runs the unity event for the selected object
                bridgeObjective++;

            }
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.tag == "BridgeSpot3" && BridgeCollection == 5 && bridgeObjective == 2) //  && BridgeCollection == 5 (all bridge pieces collected)
            {
                Debug.Log("bridgecast hit");

                bridgeBuild3.Invoke(); //runs the unity event for the selected object
                bridgeObjective++;

            }
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.tag == "BridgeSpot4" && BridgeCollection == 5 && bridgeObjective == 3) //  && BridgeCollection == 5 (all bridge pieces collected)
            {
                Debug.Log("bridgecast hit");

                bridgeBuild4.Invoke(); //runs the unity event for the selected object
                bridgeObjective++;

            }
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.tag == "BridgeSpot5" && BridgeCollection == 5 && bridgeObjective == 4) //  && BridgeCollection == 5 (all bridge pieces collected)
            {
                Debug.Log("bridgecast hit");

                bridgeBuild5.Invoke(); //runs the unity event for the selected object
                bridgeObjective++;

            }

        //Turn off raycast when user is not pressing the trigger
        } else
        {
            linePointer.enabled = false;
        }

        //when the bridge is built, update the player's waypoint script bridge objective
        if (bridgeObjective >= 5)
        {
            player.GetComponent<waypointMovement>().BridgeBuild++;
        }
    }











    //mouse testing
/*

public void OnMouseDown()
{
    
     Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
            Ray ray = new Ray(raycastObject.transform.position, fwd); //controller ray

            Debug.DrawRay(raycastObject.transform.position, fwd * 50, Color.green, 1, true);

            RaycastHit hit;

    if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && Physics.Raycast(ray, out hit, 25))
    {
        this.bridgeBuild.Invoke();
        ShootLaserFromTargetPosition(transform.position, Vector3.forward, laserMaxLength);
        laserLineRenderer.enabled = true;
    }
    else
    {
        laserLineRenderer.enabled = false;
    }
}

}

