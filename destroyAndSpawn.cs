using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT IS FOR INCREMENTING OBJECTIVE 1
//------------------------------------------------
[RequireComponent(typeof(AudioSource))] 
public class destroyAndSpawn : MonoBehaviour {

    public GameObject hitEffect;
    public GameObject animal1;

    //need to assign the camerarig in engine to update it's objectives
    public GameObject player;
    AudioSource audioData;

    void Start () {
        audioData = GetComponent<AudioSource>();
    
    }
	
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("entered");

        if ((col.gameObject.name == "cannonball(Clone)") || (col.gameObject.name == "cannonball"))
        {
            Debug.Log("Cannonball hit chest");
            audioData.Play(0);
            Debug.Log("hit+sfx play");

            //instantiate critter and particle
            Instantiate(hitEffect, transform.position, transform.rotation); //particles
            Instantiate(animal1, transform.position, transform.rotation); //critter1

            Destroy(col.gameObject); //This should destroy the cannonball instance instantly, but doesn't seem to be working. Cannonball destroys itself after a few seconds anyway
            Destroy(gameObject); //destroys chest
            


            //increase objecive1 by 1
            //Objectives for different areas are handled by seperate scripts
            player.GetComponent<waypointMovement>().objective1++;

        }
    }

}
