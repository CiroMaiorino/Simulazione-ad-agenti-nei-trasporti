using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    private Bus bus;
    private List<Agent> pendular;
    private GameObject busStop;
    
    // Start is called before the first frame update
    void Start()
    {
        bus = transform.parent.transform.parent.GetComponent<Bus>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        String colliderTag=collider.gameObject.tag;
        if (colliderTag == "StopTrigger")
        {
            bus.currentStop = collider.transform.parent.gameObject;
            busStop = collider.gameObject.transform.parent.gameObject;
            pendular = Utility<Agent>.GetAllChildren(busStop);
            List<Agent> passengers = Utility<Agent>.GetAllChildren(bus.Passengers);

            foreach (Agent agent in passengers)
            {
                if (busStop.name == "BusStop" + agent.Mystop)
                {
                    bus.StopEngine();
                    agent.GetOff();
                }
            }

            SeatsCheck();

        }
    }

    public void SeatsCheck()
    {
        if (pendular.Count != 0)
        {
            bus.StopEngine();
            if (!CheckGetOff(busStop))
                foreach (Agent agent in pendular)
                {
                    agent.TakeBus();
                }
        }
    }

    public bool CheckGetOff(GameObject currentStop)
    {
        List<Agent> passengers = Utility<Agent>.GetAllChildren(bus.Passengers);

        foreach (Agent a in passengers)
        {
            if (busStop.name == "BusStop" + a.Mystop)                   //Da sempre True se qualcuno deve scendere alla fermata
            {
                a.transform.parent = null;
                return true;
            }
        }
        return false;
    }

}
