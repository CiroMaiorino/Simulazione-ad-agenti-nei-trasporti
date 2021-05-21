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
    private Target target;
    /* Declaration of the animator to work on agent's animation. */
    private Animator animator;
    /* Declaration of IAstarAI to use A* methods. */
    private IAstarAI ai;
    /* Gameobject that contains all the passengers of the bus. */
    private AIPath aiPath;
    private GameObject passengers;
    private Seeker seeker;
    private float timeToRotate=0.05f; 
    /* Start is called before the first frame update. */
    void Start()
    {
        /* Instance of objects Declared before. */
        passengers = bus.transform.Find("Passengers").gameObject;
        destinationSetter = GetComponent<AIDestinationSetter>();
        ai = GetComponent<IAstarAI>();
        seeker=GetComponent<Seeker>();
        aiPath=GetComponent<AIPath>();
        /* Instance of the animator from the agent. */ 
        animator = this.GetComponentInChildren<Animator>();
          if (!bus.isMooving && bus.freeTarget() != null){
            target=bus.freeTarget();
            setDestination(target);
          }
            
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
        if(ai.reachedDestination ){
            transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.identity,timeToRotate);
            animator.SetBool("Waiting",true);
            animator.SetBool("Sit",true);
            
          
            bool isRotated=transform.rotation == Quaternion.identity;
            bool haveRigidBody=gameObject.GetComponent<Rigidbody>()!=null;
            //Debug.LogError("Vado al target "+ target.name+" isRotated="+isRotated+" haveRigidBody="+haveRigidBody);
            if( isRotated && !haveRigidBody){
              gameObject.AddComponent<Rigidbody>();
              aiPath.enabled = false;
            }
            transform.parent=passengers.transform;
            //Da errore nei cloni
            
              
        }
        
        
    }

    /* The method that set the target for the agents. */
    public void setDestination(Target t){
        destinationSetter.target=t.transform;
        t.isOccupied=true;
    }
    
}
