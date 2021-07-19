using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using PathCreation.Examples;

public class GameControl : MonoBehaviour
{
    public Bus bus;
    public int numeroAgenti;
    [SerializeField] GameObject busStops;
    [SerializeField] Agent agentPrefab;
    private List<Transform> stops;
    private Stop stop;

    void Start() {
        stops = Utility<Transform>.GetAllChildren(busStops);
        Stop stop = Utility<Stop>.GetAllChildren(bus.gameObject.transform.Find("Wheels").gameObject)[0];
        
    }

     void Update(){
        if (bus.GetComponent<PathFollower>().speed == 0)
            if (Utility<Agent>.GetAllChildren(bus.currentStop).Count == 0 && CanStart(Utility<Agent>.GetAllChildren(bus.Passengers)))
                 bus.StartEngine();
            
        GameActions();
    }


    public bool CanStart(List<Agent> passangers)
    {
        foreach(Agent a in passangers)
        {
            if (a.AnimatorStatus())
                return false;
        }
        return true;
    }

    void GameActions()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            for (; numeroAgenti > 0; numeroAgenti--)
            {
                SpawnAgent();
            }
        }

    }

    public void SpawnAgent(){

        int waitingSpotTmp =Random.Range(0, stops.Count);
        var position =new Vector3(Random.Range(0f, 4.0f),0,Random.Range(-2.5f,2.5f));
        
        agentPrefab.bus = bus;
        agentPrefab.Mystop = Random.Range(1, stops.Count+1);
        Instantiate(agentPrefab.gameObject,position+stops[waitingSpotTmp].transform.position,Quaternion.identity).transform.parent=stops[waitingSpotTmp];    
    }
}
