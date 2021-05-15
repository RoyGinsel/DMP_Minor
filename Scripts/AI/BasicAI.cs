using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class BasicAI : MonoBehaviour
{
    //AI
    public NavMeshAgent agent = null;
    public ThirdPersonCharacter character = null;
    public GameObject[] waypoints;
    public int waypointIndex = 0;
    public float walkSpeed;
    private bool walking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Moving()
    {
        Debug.Log("movingg!");
        Vector3 currentWaypoint = waypoints[waypointIndex].transform.position;
        Vector3 targetWaypoint = new Vector3(currentWaypoint.x, 0, currentWaypoint.z);
        agent.speed = walkSpeed;

        Debug.Log(agent.velocity);
        transform.rotation = Quaternion.LookRotation(agent.velocity, Vector3.up);
        if (Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), targetWaypoint) >= 1 && !walking)
        {
            walking = true;
            bool destination = agent.SetDestination(targetWaypoint);
            character.Move(agent.desiredVelocity, false, true);
        }
        else if (Vector3.Distance(new Vector3(this.transform.position.x, 0,this.transform.position.z), targetWaypoint) < 1 && walking)
        {
            waypointIndex += 1;
            if (waypointIndex >= waypoints.Length)
            {
                Debug.Log("agent stopping");
                waypointIndex = 0;
                walking = false;
            }
            return walking;
        }
        /*else
        {
            character.Move(Vector3.zero, false, false);
            return false;
        }*/
        return true;
    }
}
