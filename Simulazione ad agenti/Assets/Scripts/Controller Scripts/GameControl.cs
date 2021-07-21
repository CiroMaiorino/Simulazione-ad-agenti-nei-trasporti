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

            for (int i=numeroAgenti; i > 0; i--)
            {
                SpawnAgent();
            }
        }

    }

    public void SpawnAgent(){

        int waitingSpotTmp =Random.Range(0, stops.Count);
        SpawningArea spawningArea = Utility<SpawningArea>.GetAllChildren(stops[waitingSpotTmp].gameObject)[0];

        Vector3 position = spawningArea.transform.position+new Vector3(Random.Range(-spawningArea.size.x/2,spawningArea.size.x/2),
            0,
            Random.Range(-spawningArea.size.z/2,spawningArea.size.z/2));
        
        agentPrefab.bus = bus;
        agentPrefab.Mystop = Random.Range(1, stops.Count+1);
        Instantiate(agentPrefab.gameObject,position,Quaternion.identity).transform.parent=stops[waitingSpotTmp];
    }
}
