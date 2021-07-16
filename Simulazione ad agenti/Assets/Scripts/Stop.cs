using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    private Bus bus;
    private GameObject childPassengers;
    
    // Start is called before the first frame update
    void Start()
    {
      bus = transform.parent.transform.parent.GetComponent<Bus>();
      childPassengers=bus.gameObject.transform.Find("Passengers").gameObject;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "StopTrigger")
        {
            GameObject colliderParent= collider.gameObject.transform.parent.gameObject;
            List<Agent> dudes = Utility<Agent>.GetAllChildren(colliderParent);
            List<Agent> passengers = Utility<Agent>.GetAllChildren(childPassengers);
            
            if (dudes.Count != 0)
            {
                bus.WaitingPassangers();
                foreach(Agent agent in dudes)
                {
                    agent.Movement();
                }
            }
            if(passengers.Count!= 0)
            {
                foreach (Agent agent in passengers)
                {
                    if (colliderParent.name == "BusStop" + agent.mystop) 
                    {
                        
                    }
                }
            }

              
        }
    }
    }
