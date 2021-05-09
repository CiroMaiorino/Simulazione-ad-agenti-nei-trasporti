using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Agent : MonoBehaviour
{
    // Start is called before the first frame update
    AIDestinationSetter destinationSetter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setDestination(Target t){
        destinationSetter.target=t.transform;
    }
}
