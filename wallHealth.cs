using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Breakable wall health and interaction
public class wallHealth : MonoBehaviour
{

    public int health1;
    public int health2;
    public GameObject Wall1;
    public GameObject Wall2;

    public GameObject destroyEffect;
    public GameObject wallHitEffect;

    public GameObject player;

    void Update()
    {
        //if the health of the wall hits 0, destroy it
        if (health1 <= 0)
        {
            Destroy(Wall1);
            
            //continue to next waypoint
            player.GetComponent<waypointMovement>().wallObjective1++;
        }

        //same for second wall
        if (health2 <= 0)
        {   
            Destroy(Wall2);
            
            //continue to next waypoint
            player.GetComponent<waypointMovement>().wallObjective2++;
        }


    }
    

    //Detect user interaction with breakable walls
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("WallHit");

        //Check if the GameObject has a certain tag to determine which wall is being hit
        if (gameObject.tag == "Wall1")
        {
            //if the wall is hit, play a little hit effect and take away 1 health
            if (col.gameObject.name == "cannonball(Clone)")
            {
                ContactPoint contact = col.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;
                
                Instantiate(wallHitEffect, pos, rot); //particles at collision point
                health1 -= 1; //Damage
                Destroy(col.gameObject); //Destroys cannonball

                if (health1 <= 0)
                {
                    Instantiate(destroyEffect, pos, rot);
                }
            }
        }

        //Same for wall 2
        if (gameObject.tag == "Wall2")
        {
            if (col.gameObject.name == "cannonball(Clone)")
            {
                ContactPoint contact = col.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;

                Instantiate(wallHitEffect, pos, rot); //particles at collision point
                health2 -= 1;//Damage
                Destroy(col.gameObject);//Destroys cannonball

                if (health2 <= 0)
                {
                    Instantiate(destroyEffect, pos, rot);
                }
            }
        }


    }
}