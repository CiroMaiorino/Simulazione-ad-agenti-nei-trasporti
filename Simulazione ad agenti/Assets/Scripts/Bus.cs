using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using Pathfinding;

/// <summary>
///This class represents the Bus.  
/// </summary>
 
public class Bus : MonoBehaviour
{
    /// <summary>
    /// A Parameters that indicates if the Bus is moving.
    /// </summary>
    public bool isMooving;
    public GameObject TargetParent;
    /// <summary>
    ///  List of the targets
    /// </summary>
    [SerializeField] List<Target> targets;
    /// <summary>
    /// bus ramp
    /// </summary>
    private GameObject ramp;

    private void Start() {
        targets = Utility<Target>.GetAllChildren(TargetParent);
        ramp=GameObject.Find("Ramp");
        ramp.SetActive(false);
    }
  
  private void Update() {
        

  }
    public void WaitingPassangers()
    {
        isMooving = false;
        GetComponent<PathFollower>().speed = 0;
        ramp.SetActive(true);
        AstarPath.active.Scan();
    }
    /// <summary>
    /// Return the first seat aviable.
    /// </summary>
    /// <returns></returns>
    public Target freeTarget()
    {
        if (targets != null)
        {
            return targets.Find(target => target.isOccupied == false);
        }
        return null;
    }

  
    

}
