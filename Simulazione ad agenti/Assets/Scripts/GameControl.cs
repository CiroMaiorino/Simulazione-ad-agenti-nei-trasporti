using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameControl : MonoBehaviour
{

    public GameObject agentPrefab;
    public Transform Spawnpoint;
    public Bus bus;
    private Agent agent;
    public int numeroAgenti;

    void Start() {
        
    }

     void Update(){
            
            if(Input.GetKeyDown(KeyCode.P)){

                for(;numeroAgenti>0;numeroAgenti--){
                    spawnAgent();
                }
            }
    }
    public void spawnAgent(){
        
        var position =new Vector3(Random.Range(-5.0f, 5.0f),0,Random.Range(-5.0f,5.0f));

        GameObject gameOb=Instantiate(agentPrefab,position,Spawnpoint.rotation);
        agent=gameOb.GetComponent<Agent>();
        agent.bus=bus;
        
        
        
    }
    
    
}
