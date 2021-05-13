using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Agent : MonoBehaviour
{
    // Start is called before the first frame update
    AIDestinationSetter destinationSetter;
    public Pullman pullman;
    private Animator animator;
    private IAstarAI ai;
    private GameObject passengers;

    public float mammt=0f;
    void Start()
    {
        passengers = pullman.transform.Find("Passengers").gameObject;
        destinationSetter = GetComponent<AIDestinationSetter>();
        animator = this.GetComponentInChildren<Animator>();
        ai = GetComponent<IAstarAI>();

        if (!pullman.isMooving && pullman.freeTarget() != null)
            setDestination(pullman.freeTarget());
        else
            animator.SetBool("Waiting",true);

    }

    // Update is called once per frame
    void Update()
    {
        if(ai.reachedDestination){
            Debug.Log(ai.rotation);
            animator.SetBool("Waiting",true);
            transform.rotation =Quaternion.identity;
            animator.SetBool("Sit",true);
            transform.parent=passengers.transform;  
        }
        
    }

    public void setDestination(Target t){
        destinationSetter.target=t.transform;
        t.isOccupied=true;
    }
    
}
