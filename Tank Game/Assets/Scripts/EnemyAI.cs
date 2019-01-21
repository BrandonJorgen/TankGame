using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] Locs;
    private NavMeshAgent agent;
    private int destinationLoc = 0;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NextLocation();
    }

    void NextLocation()
    {
        agent.destination = Locs[destinationLoc].position; //Tells the agent to go to the location in the array
        destinationLoc = (destinationLoc + 1) % Locs.Length; //Sets up the next destination for the next func call
                                                             //Also resets to the beginning of the array if it reaches the end
    }

    void Update()
    {
        if (agent.remainingDistance < 0.25f)
        {
            NextLocation();
        }
    }
}
