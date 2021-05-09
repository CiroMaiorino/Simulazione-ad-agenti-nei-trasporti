using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour
{
    [SerializeField] List<Target> targets =new List<Target>();
    void Start()
    {
       Debug.Log(freeTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Target freeTarget(){
        if(targets != null){
            return targets.Find(target =>target.isOccupied==false);
        }
        return null;
    }
}
