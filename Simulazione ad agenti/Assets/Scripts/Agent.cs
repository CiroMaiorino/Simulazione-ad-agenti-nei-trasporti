using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/* This class represent the agent. */
public class Agent : MonoBehaviour
{
    /// <summary>
    /// Operate to set the destination of the agent.
    /// </summary>    
    AIDestinationSetter destinationSetter;
    /// <summary>
    /// Represents the Bus on the scene. 
    /// </summary>
    [HideInInspector] public Bus bus;
    /// <summary>
    /// Represents the target for the agent
    /// </summary>
   [SerializeField] private Target target;
    /// <summary>
    /// Declaration of the animator to work on agent's animation.
    /// </summary>
    private Animator animator;
    /// <summary>
    /// Declaration of IAstarAI to use A* methods.
    /// </summary>
    private IAstarAI ai;
    /// <summary>
    ///  Gameobject that contains all the passengers of the bus.
    /// </summary>
    private AIPath aiPath;
    private GameObject passengers;
    private float elapsedTime=0f;

  
    [SerializeField] private int mystop;

    public int Mystop { get => mystop; set => mystop = value; }

    void Start()
    {
        
        /* Instance of objects Declared before. */
        passengers = bus.transform.Find("Passengers").gameObject;
        destinationSetter = GetComponent<AIDestinationSetter>();
        ai = GetComponent<IAstarAI>();
        aiPath = GetComponent<AIPath>();
        /* Instance of the animator from the agent. */
        animator = this.GetComponentInChildren<Animator>();
        
    }
    public void Movement()
    {
        /* If the bus is not moving and there are free seats the agents will set
      their target to one of the free seat. */
        if (!bus.isMooving && bus.freeTarget() != null)
        {
            animator.SetBool("Waiting", false);
            target = bus.freeTarget();
            setDestination(target);
        }
    }
    public void getOff()
    {
        Debug.LogError("getDown");
        animator.SetBool("Waiting", false);
        animator.SetBool("Sit", false);
        Destroy(GetComponent<Rigidbody>());
        aiPath.enabled = true;
        target.IsOccupied = false;
        setDestination(bus.Exit);
        
    }
    IEnumerator RotateOnSpot()
    {
        while (elapsedTime< 10f) {
            transform.rotation = Quaternion.Lerp(transform.rotation, bus.transform.rotation, elapsedTime/10f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    void Update()
    {
        /* When the agents arrive at the target change the animation to waiting
         rotate him and make him sit. */
        if(ai.reachedDestination){
            if (bus.Seats.Contains(target))
                sitDown();
            else { 
                getOff(); 
                
                Destroy(gameObject); 
            }
        }
        
        
    }

    /// <summary>
    /// The method that set the target for the agents.  
    /// </summary>

    public void setDestination(Target t){
        target = t;
        destinationSetter.target=t.transform;
        t.IsOccupied=true;
    }

    private void sitDown()
    {
        
        StartCoroutine(RotateOnSpot());
        animator.SetBool("Waiting", true);
        animator.SetBool("Sit", true);


        bool isRotated = (transform.rotation.y - bus.transform.rotation.y) <= 0.1;
        bool haveRigidBody = gameObject.GetComponent<Rigidbody>() != null;

        if (isRotated && !haveRigidBody)
        {
            gameObject.AddComponent<Rigidbody>().freezeRotation = true;

            aiPath.enabled = false;
        }
        transform.parent = passengers.transform;
    }

    private void OnDestroy()
    {
        Stop stop=Utility<Stop>.GetAllChildren(bus.gameObject.transform.Find("Wheels").gameObject)[0];
        stop.SeatsCheck();
        
    }

}
