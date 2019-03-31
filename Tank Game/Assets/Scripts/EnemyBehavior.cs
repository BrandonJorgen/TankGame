using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Health Settings")] 
    public FloatData MaxHits;
    public float CurrentHits;
    public UnityEvent ReachedMaxHits;

    [Header("Line of Sight Settings")] 
    public float Range = 2f;
    public float FiringRange = 1f;
    private bool inRange = false;
    private bool wasSighted = false;
    private bool inSight = false;
    private bool inFiringRange = false;
    public UnityEvent PlayerSighted;
    private Vector3 enemyLoc, playerLoc;
    private GameObject player;
    private float currentDistance;
    private Vector3 lastKnownPosition;
    private RaycastHit inSightHit, lastKnownHit;
    private bool isReloading = false;//Used in reloading cooldown coroutine

    [Header("Attack Settings")] 
    public GameObject Shell;
    public Transform ShellOrigin;
    public float ReloadSpeed = 1f;
    private bool isAttacking;
    public UnityEvent LostPlayer;

    [Header("Patrol Settings")] 
    [Tooltip("How fast the enemy moves, 0 = no movement")]
    public float Speed = 3.5f;
    [Tooltip("How fast the enemy returns to last location before seeing player")]
    public float ReturnSpeed = 1.75f;
    public bool WillPatrol = false;//Only here so I can see if it patrols in the inspector
    public Transform[] Locations;//List of waypoints along the patrol route
    private NavMeshAgent agent;
    private int destinationPoint = 0;//Starting point for Locations array
    private Vector3 originPosition;
    private float originRotationY;
    private Animator animator;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, FiringRange);
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        originPosition = transform.position;//Grabs the position the enemy spawns at
        originRotationY = transform.eulerAngles.y;//Grabs the angle the enemy spawns at
        
        if (Locations.Length > 1)//Checking if a successful patrol route is set
        {
            WillPatrol = true;//More than 1 location set
        }
        else
        {
            WillPatrol = false;//Less than 2 locations set, will not patrol
        }

        NextLocation();//Start Patrol Code
        InvokeRepeating("PlayerSearch", 2f, 0.5f);//Start looking for the player after 2.5 seconds and every 0.5 seconds
    }

    void Update()
    {
        Debug.DrawLine(transform.position, agent.destination, Color.cyan);
        Debug.DrawLine(transform.position, inSightHit.point, Color.green);
        Debug.DrawLine(transform.position, lastKnownHit.point, Color.black);

        //Enemy Death Check Event
        if (CurrentHits >= MaxHits.Value)
        {
            ReachedMaxHits.Invoke();
        }

        //Patrol Code
        enemyLoc = transform.position; //Find the current location of the enemy
        playerLoc = player.transform.position; //Find the current location of the Player
        currentDistance = Vector3.Distance(enemyLoc, playerLoc); //Find how far they are from each other

        //If the enemy gets close enough to current destination, call function
        //Shouldn't call this if the enemy is attacking the player
        if (gameObject.activeSelf && !isAttacking && agent.remainingDistance < 0.25f)
        {
            NextLocation();
        }

        //Casts ray at player and returns hit
        if (Physics.Linecast(transform.position, playerLoc, out inSightHit))
        {
            //Goal here is to always be seeing if the player is in
            //logical sight (if there is ANYTHING between the enemy and the player) of the enemy
            //We check if the player is in actual sight of the enemy in the PlayerSearch function
            if (inSightHit.transform.CompareTag("Player"))
            {
                inSight = true;
            }
            else
            {
                inSight = false;
            }
        }

        //If the enemy returns to its origin while not attacking the player
        //and its not set to patrol, reset its speed
        if (!isAttacking && Locations.Length <= 1)
        {
            if (Vector3.Distance(transform.position, originPosition) <= 0.25f)
            {
                agent.speed = Speed;
                
                if (agent.remainingDistance <= 0f)
                {
                    ResetRotation();
                }
            }
        }
        
        //Animation Code
        if (agent.velocity.x < 0)//Moving Left
        {
            animator.SetBool("MovingLeft", true);
            animator.SetBool("MovingRight", false);
        }

        if (agent.velocity.x > 0)//Moving Right
        {
            animator.SetBool("MovingLeft", false);
            animator.SetBool("MovingRight", true);
        }

        if (agent.velocity.z < 0)//Moving Down
        {
            animator.SetBool("MovingDown", true);
            animator.SetBool("MovingUp", false);
        }

        if (agent.velocity.z > 0)//Moving Up
        {
            animator.SetBool("MovingDown", false);
            animator.SetBool("MovingUp", true);
        }
    }

    private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("PlayerShell")) //Did we get hit by a player shell
            {
                //Add the floatdata value of the player shell to the current hit counter
                CurrentHits += other.gameObject.GetComponent<DestroyShell>().ShellDamage.Value;
            }
        }

        void NextLocation()
        {
            //Will not patrol if less than 2 locations are set
            if (Locations.Length <= 1)
            {
                return;
            }
            agent.destination = Locations[destinationPoint].position; //Send to point
            destinationPoint = (destinationPoint + 1) % Locations.Length; //Changes point to send to and resets if it reaches the end
            agent.speed = Speed;
        }

        void PlayerSearch()//Enemy is currently looking for the Player
        {
            //Determines if the player is in enemy's range
            if (currentDistance <= Range)
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }

            //Determines if the player is in the enemy's firing range
            if (currentDistance <= FiringRange)
            {
                inFiringRange = true;
            }
            else
            {
                inFiringRange = false;
            }
            
            //Determine if the player is in enemy's sight
            if (inRange && inSight)
            {
                Physics.Linecast(transform.position, playerLoc, out lastKnownHit);

                //Setting the last position the enemy saw the player at
                if (gameObject.activeSelf && lastKnownHit.transform.CompareTag("Player"))
                {
                    agent.speed = Speed;
                    lastKnownPosition = lastKnownHit.point;
                }
                
                wasSighted = true;
                PlayerSighted.Invoke();//Reserved for Visual representation that the player was spotted

                if (inFiringRange)
                {
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(Firing());
                    }
                }
            }

            //The enemy saw the player at some point
            if (wasSighted)
            {
                if (gameObject.activeSelf)
                {
                    agent.destination = lastKnownPosition;//Go to last known position
                }
                
                isAttacking = true;
                
                if (Vector3.Distance(transform.position, lastKnownPosition) <= 1f)//Has reached player's last known location
                {
                    StartCoroutine(LastKnownSearch());
                }
            }
        }

    IEnumerator LastKnownSearch()
    {
        //Acts as a looking around for player timer
        LostPlayer.Invoke();//Reserved for visual representation that the player has hidden from the enemy
        yield return new WaitForSeconds(1f);
        wasSighted = false;

        if (Locations.Length <= 1)
        {
            agent.speed = ReturnSpeed;
            agent.destination = originPosition;
        }
        else
        {
            agent.speed = ReturnSpeed;
        }
        
        isAttacking = false;
    }
    
    IEnumerator Firing()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).LookAt(playerLoc);
        if (!isReloading)
        {
            //May end of changing this depending on how I set up sprites
            Instantiate(Shell, ShellOrigin.position, ShellOrigin.rotation);
            StartCoroutine(Reloading());
        }

        if (inRange && !inSight)
        {
            agent.isStopped = false;
        }

        if (!inFiringRange)
        {
            agent.isStopped = false;
        }
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(ReloadSpeed);
        isReloading = false;
    }

    void ResetRotation()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, originRotationY, transform.eulerAngles.z);
    }
}