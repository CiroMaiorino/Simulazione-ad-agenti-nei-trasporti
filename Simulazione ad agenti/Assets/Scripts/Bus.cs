using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

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
    }
  
  private void Update() {
      if(isMooving)
        ramp.SetActive(false);
        else ramp.SetActive(true);
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
