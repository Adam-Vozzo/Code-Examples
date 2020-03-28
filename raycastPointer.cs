using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handle user shooting, direction, cooldown, visual and audio effects
public class raycastPointer : MonoBehaviour {

    public GameObject raycastObject;
    public GameObject cannonball;
    public GameObject controller;
    public float fireRate;
    public GameObject fireEffect;

    private float timeToFire = 0;

    AudioSource audioData;

    void Start () {
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    { //INPUT 

        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);

        Ray ray = new Ray (raycastObject.transform.position, fwd);

        Debug.DrawRay(raycastObject.transform.position, fwd * 50, Color.green, 1, true);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && Time.time >= timeToFire) // add to test with pc -  || Input.GetMouseButtonDown(0)
        {
            timeToFire = Time.time + 1 / fireRate; //Start cooldown on ability
            Instantiate(cannonball, raycastObject.transform.position, raycastObject.transform.rotation); //Create cannonball in the correct position, velocity is handled in a seperate script
            Instantiate(fireEffect, raycastObject.transform.position, raycastObject.transform.rotation); //play effect
            audioData.Play(0);
            Debug.Log("fire+sfx play");
        }

    }
}
