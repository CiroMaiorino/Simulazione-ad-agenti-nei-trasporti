using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using PathCreation.Examples;

public class GameControl : MonoBehaviour
{

    public Agent agentPrefab;
    public Transform Spawnpoint;
    public Bus bus;
    public int numeroAgenti;
    private int waitingSpotTmp;
    [SerializeField] GameObject busStops;
    private List<Transform> stops;
    
    void Start() {
        stops = new List<Transform>();
        foreach (Transform child in busStops.transform)
        {
            stops.Add(child);
        }
    }

     void Update(){
            
            if(Input.GetKeyDown(KeyCode.P)){

                for(;numeroAgenti>0;numeroAgenti--){
                    spawnAgent();
                }
            }
            if(Input.GetKeyDown(KeyCode.M)){
                bus.isMooving=true;
                bus.transform.position=new Vector3(0,0.63f,0);
                
            }
    }
    public void spawnAgent(){

        waitingSpotTmp=Random.Range(0, 4);
        var position =new Vector3(Random.Range(0f, 4.0f),0,Random.Range(-2.5f,2.5f));

        agentPrefab.bus = bus;
        Instantiate(agentPrefab.gameObject,position+stops[waitingSpotTmp].transform.position,Spawnpoint.rotation);
        
      
           
    }

    /*
     * Se bus è alla fermata x di un array di fermate (Magari presa da posizione) stop del bus   O tramite linea sul terreno che fa da trigger
     * 
     * Se un omino sta camminando sta camminando ferma il bus (Sull'idea di farli spawnare alla fermata) 
     * un set di fermata a cui scendere presa a random dagli omini quando salgono se il bus e alla loro scendono
     * il punto di spawn degli omini sarà una fermata randomica alla pressione di un tasto.*/
}
