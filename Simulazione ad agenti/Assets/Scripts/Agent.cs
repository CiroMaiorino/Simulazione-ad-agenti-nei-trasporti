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
    private float elapsedTime=0f;
    private Material material;
    private GameObject particlesSystem;
    private GameControl gameControl;
    private GameObject rigidCube;
    public enum States
    {
       Healthy,
        Infected,
        Contagious
    }
    [HideInInspector] public States State;
    [SerializeField] private int mystop;

    public int Mystop { get => mystop; set => mystop = value; }
    

    private void Start()
    {
        /* Instance of objects Declared before. */
        destinationSetter = GetComponent<AIDestinationSetter>();
        ai = GetComponent<IAstarAI>();
        aiPath = GetComponent<AIPath>();
        rigidCube = GetComponentInChildren<SphereCollider>().gameObject;
        /* Instance of the animator from the agent. */
        animator = this.GetComponentInChildren<Animator>();
        material = GetComponentInChildren<Transform>().GetComponentInChildren<Renderer>().material;
        particlesSystem = GetComponentInChildren<Illness>().gameObject;

        particlesSystem.SetActive(false);
        gameControl = FindObjectOfType<GameControl>();
        Contagious();
        
    }
    public void TakeBus()
    {
        /* If the bus is not moving and there are free seats the agents will set
      their target to one of the free seat. */
        if (!bus.isMooving && bus.FreeTarget() != null && target== null)
        {
            animator.SetBool("Waiting", false);
            target = bus.FreeTarget();
            SetDestination(target);
        }
    }
    public void GetOff()
    {
        animator.SetBool("Waiting", false);
        animator.SetBool("Sit", false);
        target.IsOccupied = false;
        SetDestination(bus.Exit);
        
    }
    IEnumerator RotateOnSpot()
    {
        while (elapsedTime< 10f) {
            transform.rotation = Quaternion.Lerp(transform.rotation, bus.transform.rotation, elapsedTime/10f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void Update()
    {
        /* When the agents arrive at the target change the animation to waiting
         rotate him and make him sit. */
        if(ai.reachedDestination){
            if (bus.Seats.Contains(target))
                SitDown();
            
        }
        
        
    }

    public void Exiting()
    {
        Stop stop = Utility<Stop>.GetAllChildren(bus.gameObject.transform.Find("Wheels").gameObject)[0];
        stop.SeatsCheck();
        Destroy(gameObject);

        if (State == States.Healthy)
            gameControl.addHealthy();
    }

    /// <summary>
    /// The method that set the target for the agents.  
    /// </summary>
    public bool AnimatorStatus()
    {
        Animator anim = GetComponentInChildren<Animator>();

        if (anim.GetBool("Sit"))
        {
            return false;
        }
        else return true;
    }
    public void SetDestination(Target t){
        target = t;
        destinationSetter.target=t.transform;
        transform.parent = bus.Passengers.transform;
        if (t != bus.Exit)
        { t.IsOccupied = true;
         
        }
    }

    private void SitDown()
    {
        if (State == States.Healthy)
        {
            gameObject.AddComponent<ColliderCovid>();
            GetComponent<ColliderCovid>().InfectionPercentage = gameControl.infectionPercentage;
        }
        StartCoroutine(RotateOnSpot());
        aiPath.enabled = false;
        animator.SetBool("Waiting", true);
        animator.SetBool("Sit", true);
        rigidCube.tag = "Exiting";
       
        transform.position = target.transform.position;
        bool haveRigidBody = gameObject.GetComponent<Rigidbody>() != null;

        if (!haveRigidBody)
        {
            gameObject.AddComponent<Rigidbody>().freezeRotation = true;

            
        }
        
    }

    public void Contagious()
    {
        if (State == States.Contagious)
        {
            material.color = Color.red;
            particlesSystem.SetActive(true);
            GetComponentInChildren<ColliderCovid>().gameObject.SetActive(false);

        }
    }
    public void Infected()
    {
        State = States.Infected;
        gameControl.addInfected();
        material.color = Color.yellow;
        Destroy(GetComponent<ColliderCovid>());
        if (GetComponentInChildren<ColliderCovid>() != null)
            Destroy(GetComponentInChildren<ColliderCovid>().gameObject);
    }
}
