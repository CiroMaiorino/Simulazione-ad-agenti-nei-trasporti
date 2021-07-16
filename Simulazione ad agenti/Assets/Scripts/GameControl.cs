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
    }

     void Update(){
        GameActions();
    }


    void GameActions()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            for (; numeroAgenti > 0; numeroAgenti--)
            {
                spawnAgent();
            }
        }

    }

    public void spawnAgent(){

        int waitingSpotTmp =Random.Range(0, stops.Count);
        var position =new Vector3(Random.Range(0f, 4.0f),0,Random.Range(-2.5f,2.5f));
        
        agentPrefab.bus = bus;
        agentPrefab.mystop = Random.Range(1, stops.Count+1);
        Instantiate(agentPrefab.gameObject,position+stops[waitingSpotTmp].transform.position,Quaternion.identity).transform.parent=stops[waitingSpotTmp];    
    }

    /*
     * Se bus � alla fermata x di un array di fermate (Magari presa da posizione) stop del bus   O tramite linea sul terreno che fa da trigger
     * 
     * Se un omino sta camminando sta camminando ferma il bus (Sull'idea di farli spawnare alla fermata) 
     * un set di fermata a cui scendere presa a random dagli omini quando salgono se il bus e alla loro scendono
     * il punto di spawn degli omini sar� una fermata randomica alla pressione di un tasto.*/
}
