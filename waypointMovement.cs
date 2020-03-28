using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement of the player through waypoints and progression
public class waypointMovement : MonoBehaviour
{

    public GameObject[] waypoints;

    public GameObject[] critterWaypoints;
    public GameObject[] critterObstacles;

    public int current = 0;
    public int critterCurrent = 0;

    float rotSpeed;
    public float speed;
    public float turnSpeed;
    float WPRadius = 10;

    //editable from other scripts, but not visible in editor
    [HideInInspector] public int objective1; //part 1
    [HideInInspector] public int objective2; //part 2
    [HideInInspector] public int objective3; //Part 3
    [HideInInspector] public int objective4; //part 4
    [HideInInspector] public int objective5; //part 5
    [HideInInspector] public int autoObjective; //track auto-waypoints

    public int wallObjective1;
    public int wallObjective2;
    public int BridgeBuild;

    public int objective1Goal = 0;
    public int objective2Goal = 0;
    public int objective3Goal = 0;
    public int objective4Goal = 0;
    public int objective5Goal = 0;
    public int BridgeBuildGoal = 0;
    public int autoObjectiveGoal = 1;


    //Check what objective the player is up to
    //objectives updated in their own scripts (destroyandspawn scripts)


    void Update()
    {

        Debug.Log(current);

        //always activate the current critter waypoint
        critterWaypoints[critterCurrent].SetActive(true);

        //if we arrive at the waypoint 
        //Bug: If the player somehow completes an objective while not at the correct waypoint, they will get stuck at the following waypoint, unable to proceed
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPRadius)
        {

            //and if the objectives are met for an area
            if (objective1 == objective1Goal || 
                objective2 == objective2Goal || 
                objective3 == objective3Goal || 
                objective4 == objective4Goal || 
                objective5 == objective5Goal || 
                wallObjective1 == 1 || 
                wallObjective2 == 1 || 
                BridgeBuild == BridgeBuildGoal)
            {

                //set the next waypoint as the destination
                current++;
                Debug.Log("Next Waypoint");
                Debug.Log("CritterCurrent went up to: " + (critterCurrent+1));

                //send critter to the next waypoint, and destroy old waypoints and obstacles that were preventing progression
                Destroy(critterWaypoints[critterCurrent]);
                Destroy(critterObstacles[critterCurrent]);
                critterCurrent++;
                //critterWaypoints[critterCurrent].SetActive(true);


                //Reset Objectives so the conditions aren't perpetually met
                //This system is working, but needs to be adjusted if more objectives are added
                objective1 = 0;
                objective2 = 0;
                objective3 = 0;
                objective4 = 0;
                objective5 = 0;
                wallObjective1 = 0;
                wallObjective2 = 0;
                BridgeBuild = 0;



                //send Player back to the start if players somehow iterate progression beyond the end of the level
                if (current > waypoints.Length)
                {
                    current = 0;

                }
                //Prevent critters wondering off at the end of the level (probably not needed)
                if (critterCurrent > critterWaypoints.Length)
                {
                    critterCurrent = critterWaypoints.Length; ///stay at final waypoint

                }


            }

        }

        //take the user to the next waypoint. (after a short delay) [movetowards and rotatetowards]
        //https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
        //else rotate and move the player towards the next waypoint
        else
        {
            Vector3 direction = waypoints[current].transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.time); //lerp the turn so it's not too fast
        }

        //move towards the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);


    }
    
    private void OnTriggerEnter(Collider other)
    {

        //auto-movement waypoints that guide the user through sections of the experience
        if (other.tag == "PlayerAutoMove")
        {
            Debug.Log("Player Auto-moving to next waypoint");
            current++;
            autoObjective++;

            //critter movement
            Destroy(critterWaypoints[critterCurrent]);
            //Destroy(critterObstacles[critterCurrent]);
            critterCurrent++;

            autoObjective = 0;

        }
    }
    
}
