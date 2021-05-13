using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour
{
    /* List of the Targets modificable on the inspector.*/ 
    [SerializeField] List<Target> targets =new List<Target>();

     /* Start is called before the first frame update. */
    void Start()
    {
      
    }

    /* Update is called once per frame. */
    void Update()
    {
        
    }
    /* Method that returns a list of the targets free. */
    public Target freeTarget(){
        if(targets != null){
            return targets.Find(target =>target.isOccupied==false);
        }
        return null;
    }
}
