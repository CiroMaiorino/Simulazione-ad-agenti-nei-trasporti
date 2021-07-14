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
    public Bus bus;
    /// <summary>
    /// Represents the target for the agent
    /// </summary>
    private Target target;
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
    private Seeker seeker;
    private float timeToRotate=0.05f;
   
    void Start()
    {
        /* Instance of objects Declared before. */
        passengers = bus.transform.Find("Passengers").gameObject;
        destinationSetter = GetComponent<AIDestinationSetter>();
        ai = GetComponent<IAstarAI>();
        seeker = GetComponent<Seeker>();
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
    void Update()
    {
        /* When the agents arrive at the target change the animation to waiting
         rotate him and make him sit. */
        if(ai.reachedDestination ){
            transform.rotation=Quaternion.Slerp(transform.rotation, bus.transform.rotation, timeToRotate);
            animator.SetBool("Waiting",true);
            animator.SetBool("Sit",true);
            
            
            bool isRotated = (transform.rotation.y - bus.transform.rotation.y)<=0.1;
            bool haveRigidBody = gameObject.GetComponent<Rigidbody>() != null;

            if( isRotated && !haveRigidBody){
              gameObject.AddComponent<Rigidbody>();
              aiPath.enabled = false;
            }
            transform.parent=passengers.transform;
            
            
              
        }
        
        
    }

    /// <summary>
    /// The method that set the target for the agents.  
    /// </summary>

    public void setDestination(Target t){
        destinationSetter.target=t.transform;
        t.isOccupied=true;
    }
    
}
