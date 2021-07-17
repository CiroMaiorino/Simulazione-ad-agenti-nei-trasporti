using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    private Bus bus;
    private GameObject childPassengers;
    private List<Agent> pendular;
    private GameObject busStop;
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
            bus.currentStop = collider.transform.parent.gameObject;
           busStop=collider.gameObject.transform.parent.gameObject;
             pendular = Utility<Agent>.GetAllChildren(busStop);
            List<Agent> passengers = Utility<Agent>.GetAllChildren(childPassengers);
           
            if (passengers.Count != 0)
            {
                foreach (Agent agent in passengers)
                {
                    if (busStop.name == "BusStop" + agent.Mystop)
                    {
                       // bus.WaitingPassangers();
                        agent.getOff();
                    }
                }
            }
            SeatsCheck();






        }
    }

    public void SeatsCheck()
    {
        if (pendular.Count != 0)
        {
            bus.WaitingPassangers();
            if (!CheckGetOff(busStop))
                foreach (Agent agent in pendular)
                {
                    agent.Movement();
                }
        }
    }

    public bool CheckGetOff(GameObject currentStop)
    {
        foreach(Agent a in Utility<Agent>.GetAllChildren(childPassengers))
        {
            if (busStop.name == "BusStop" + a.Mystop)
            {   
                return true;
            }
        }
        return false;
    }

    }
