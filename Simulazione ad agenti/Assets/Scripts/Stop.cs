using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TurnTheGameOn.Timer;


public class Stop : MonoBehaviour
{
    private Bus bus;
    private List<Agent> pendular;
    private GameObject busStop;
    private GameControl gameControl;
    private int runNumber =0;
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
            Onstop(collider, bus.Returning);

        }
        else if (colliderTag == "StopTriggerR")
        {
            bus.Returning = true;
           
            Onstop(collider, bus.Returning);
        }
        if (colliderTag == "NewRun")
        {
            if(runNumber!=0)
                gameControl.writer.writeStat();
            gameControl.ResetStats();
            

            Timer timer = FindObjectOfType<Timer>();
            runNumber += 1;
            if (runNumber == 8)
            {
                Time.timeScale = 0;
            }
            else if (runNumber == 5)
            {
                timer.SetTimerValue(51000);
            }
            else if (runNumber < 5)
            {
                if (((timer.GetTimerValue() - (20 * 60)) % 3600) != 0)
                    timer.AddTime(3600 - (timer.GetTimerValue() - (20 * 60)) % 3600);

                timer.AddTime(600);
            }
            else if (runNumber > 5 && runNumber < 8)
            {
                if ((timer.GetTimerValue() % 3600) != 0)
                    timer.AddTime(3600 - (timer.GetTimerValue() % 3600));

                timer.AddTime(600);
            }


            bus.Returning = false;
            gameControl.SpawningAtStops();
        }
    }



    public void SeatsCheck()
    {
        if (pendular.Count != 0)
        {
           
            if (!CheckGetOff(busStop))
            {
                if (bus.GetComponent<PathFollower>().speed != 0)
                    bus.StopEngine();
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
        //pendular.ForEach(x => x.TakeBus());
        //yield return null;
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
            yield return new WaitForSeconds(5f);
        }
       
    }
    private void Onstop(Collider collider, bool returning)
    {
        bus.currentStop = collider.transform.parent.gameObject;
        busStop = collider.gameObject.transform.parent.gameObject;
        SpawningArea area = bus.getArea();
        pendular = Utility<Agent>.GetAllChildren(area.gameObject);
        List<Agent> passengers = Utility<Agent>.GetAllChildren(bus.Passengers);
        foreach (Agent agent in passengers)
        {
            if (busStop.name == "BusStop" + agent.Mystop)
            {
               if(bus.GetComponent<PathFollower>().speed!=0)
                    bus.StopEngine();
                agent.GetOff();
            }
        }

        SeatsCheck();
    }
}