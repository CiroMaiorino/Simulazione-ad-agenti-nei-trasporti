using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Stop : MonoBehaviour
{
    private Bus bus;
    private List<Agent> pendular;
    private GameObject busStop;
    private GameControl gameControl;
    // Start is called before the first frame update
    void Start()
    {
        gameControl = FindObjectOfType<GameControl>();
        bus = transform.parent.transform.parent.GetComponent<Bus>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        String colliderTag = collider.gameObject.tag;
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
        if (colliderTag == "NewRun")
        {
             gameControl.SpawningAtStops();
        }
    }

    public void SeatsCheck()
    {
        if (pendular.Count != 0)
        {
            bus.StopEngine();
            if (!CheckGetOff(busStop))
            {
                    StartCoroutine(TakeBusCoroutine());
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



    private IEnumerator TakeBusCoroutine()
    {
        int w = 0;
        while (w <= pendular.Count)
        {
            if (pendular.Count - w < 4)
            {
                pendular.GetRange(w, pendular.Count - w).ForEach(x => x.TakeBus());
            }

            else
            {
                pendular.GetRange(w, 4).ForEach(x => x.TakeBus());
            }
            if (w + 4 > pendular.Count)
                w = pendular.Count;

            w += 4;
            yield return new WaitForSeconds(4f);
        }

    }
}