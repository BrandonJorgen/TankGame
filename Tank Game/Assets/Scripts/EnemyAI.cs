using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] Locs;
    private NavMeshAgent agent;
    private int destinationLoc = 0;

    public bool MovingRight, MovingLeft, MovingUp, MovingDown;
    
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
        //Start movement direction code
        if (agent.velocity.x > 0)
        {
            MovingRight = true;
            MovingLeft = false;
        } else if (agent.velocity.y > 0)
        {
            MovingUp = true;
            MovingDown = false;
        } else if (agent.velocity.y < 0)
        {
            MovingDown = true;
            MovingUp = false;
        }

        if (agent.velocity.x < 0)
        {
            MovingLeft = true;
            MovingRight = false;
        } else if (agent.velocity.y > 0)
        {
            MovingUp = true;
            MovingDown = false;
        } else if (agent.velocity.y < 0)
        {
            MovingDown = true;
            MovingUp = false;
        }

        if (agent.velocity.z > 0)
        {
            MovingUp = true;
            MovingDown = false;
        } else if (agent.velocity.x > 0)
        {
            MovingRight = true;
            MovingLeft = false;
        } else if (agent.velocity.x < 0)
        {
            MovingLeft = true;
            MovingRight = false;
        }

        if (agent.velocity.z < 0)
        {
            MovingDown = true;
            MovingUp = false;
        } else if (agent.velocity.x > 0)
        {
            MovingRight = true;
            MovingLeft = false;
        } else if (agent.velocity.x < 0)
        {
            MovingLeft = true;
            MovingRight = false;
        }
        //End movement direction code
        
        if (agent.remainingDistance < 0.25f)
        {
            NextLocation();
        }
    }
}
