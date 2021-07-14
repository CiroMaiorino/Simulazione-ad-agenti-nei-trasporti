using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    private Bus bus;
    
    // Start is called before the first frame update
    void Start()
    {
      bus = transform.parent.transform.parent.GetComponent<Bus>();       
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "StopTrigger")
        {
            GameObject colliderParent= collider.gameObject.transform.parent.gameObject;
            List<Agent> dudes = Utility<Agent>.GetAllChildren(colliderParent);
            
            if (dudes.Count != 0)
            {
                bus.WaitingPassangers();
                foreach(Agent agent in dudes)
                {
                    agent.Movement();
                }
            }

              
        }
    }
    }
