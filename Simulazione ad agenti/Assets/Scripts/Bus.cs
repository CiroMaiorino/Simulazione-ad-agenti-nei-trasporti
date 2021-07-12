using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

/*This class represents the Bus. */ 
public class Bus : MonoBehaviour
{
    /* A Parameters that indicates if the Bus is moving. */
    public bool isMooving;
    /* List of the targets */
    public List<Target> targets;
    private GameObject ramp;

    private void Start() {
        ramp=GameObject.Find("Ramp");
    }
  
  private void Update() {
      if(isMooving)
        ramp.SetActive(false);
        else ramp.SetActive(true);
  }
  /* Return the first seat aviable. */
  
    public Target freeTarget()
    {
        if (targets != null)
        {
            return targets.Find(target => target.isOccupied == false);
        }
        return null;
    }
    private void onTriggerEnter(Collider collider)
    {
        if (collider.tag is "StopTrigger")
        {
            Debug.Log("Firmt");
            isMooving = false;
            GetComponent<PathFollower>().speed = 0;
        }
    }


}
