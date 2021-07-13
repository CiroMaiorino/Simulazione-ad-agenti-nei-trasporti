using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour
{
    /// <summary>
    /// List of the Targets modificable on the inspector. 
    /// </summary>
    [SerializeField] List<Target> targets =new List<Target>();

    void Start()
    {
      
    }

    void Update()
    {
        
    }
    /// <summary>
    /// Method that returns a list of the targets free. 
    /// </summary>
    public Target freeTarget(){
        if(targets != null){
            return targets.Find(target =>target.isOccupied==false);
        }
        return null;
    }
}
