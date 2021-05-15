using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/* This class represent the agent. */
public class Agent : MonoBehaviour
{
    /* Operate to set the destination of the agent.*/
    AIDestinationSetter destinationSetter;
    /* Represents the Bus on the scene. */
    public Bus bus;
    /* Declaration of the animator to work on agent's animation. */
    private Animator animator;
    /* Declaration of IAstarAI to use A* methods. */
    private IAstarAI ai;
    /* Gameobject that contains all the passengers of the bus. */
    private GameObject passengers;
    private Seeker seeker;

    /* Start is called before the first frame update. */
    void Start()
    {
        /* Instance of objects Declared before. */
        passengers = bus.transform.Find("Passengers").gameObject;
        destinationSetter = GetComponent<AIDestinationSetter>();
        ai = GetComponent<IAstarAI>();
        seeker=GetComponent<Seeker>();
        /* Instance of the animator from the agent. */ 
        animator = this.GetComponentInChildren<Animator>();
          if (!bus.isMooving && bus.freeTarget() != null)
            setDestination(bus.freeTarget());
        else
        /* Change animation of the agents to waiting. */
            animator.SetBool("Waiting",true);


    }

    /* Update is called once per frame*/
    void Update()
    {
        /* If the bus is not moving and there are free seats the agetns will set
         their target to one of the free seat. */
      

        /* When the agents arrive at the target change the animation to waiting
         rotate him and make him sit. */
        if(ai.reachedDestination){
            animator.SetBool("Waiting",true);
            transform.rotation =Quaternion.identity;
            animator.SetBool("Sit",true);
            transform.parent=passengers.transform;
            seeker.CancelCurrentPathRequest();
              
        }
        
        
    }

    /* The method that set the target for the agents. */
    public void setDestination(Target t){
        destinationSetter.target=t.transform;
        t.isOccupied=true;
    }
    
}
