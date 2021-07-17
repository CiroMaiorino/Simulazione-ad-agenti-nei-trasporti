using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour
{
    /// <summary>
    /// List of the Targets modificable on the inspector. 
    /// </summary>
    [SerializeField] List<Target> targets =new List<Target>();

    /// <returns>
    /// Returns a free target. 
    /// </returns>
    public Target freeTarget(){
        if(targets != null){
            return targets.Find(target =>target.IsOccupied==false);
        }
        return null;
    }
   


   
}
