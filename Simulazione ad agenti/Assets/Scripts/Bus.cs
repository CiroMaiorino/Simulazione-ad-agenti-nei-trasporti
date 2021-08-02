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
    public int maxPassengers;
    /// <summary>
    ///  List of the targets
    /// </summary>
    private List<Target> seats;
    private Target exit;

    /// <summary>
    /// bus ramp
    /// </summary>
    private GameObject ramp;
    public GameObject currentStop;
    private GameObject passengers;
    private GridGraph gridGraph;
    public Target Exit { get => exit;}
    public List<Target> Seats { get => seats; set => seats = value; }
    public GameObject Passengers { get => passengers; set => passengers = value; }

    private void Start() {
        Seats = Utility<Target>.GetAllChildren(TargetParent);
        exit = Seats[Seats.Count - 1];
        Seats.RemoveAt(Seats.Count - 1);
        gridGraph = AstarPath.active.data.gridGraph;     
        ramp=GameObject.Find("Ramp");
        ramp.SetActive(false);
        Passengers = GameObject.Find("Passengers");
    }
  
  
    public void StopEngine()
    {
        isMooving = false;
        GetComponent<PathFollower>().speed = 0;
        gridGraph.center = Vector3.Scale(transform.position,new Vector3(1,0,1));
        ramp.SetActive(true);
        AstarPath.active.Scan();
    }

    public void StartEngine()
    {
        StartCoroutine("StartEngineCourutine");
    }
    private IEnumerator StartEngineCourutine()
    {
        yield return new WaitForSeconds(4f);
        isMooving = true;
        GetComponent<PathFollower>().speed =currentStop.GetComponentInChildren<SpawningArea>().restartSpeed;
        ramp.SetActive(false);

    }
    /// <summary>
    /// Return the first seat aviable.
    /// </summary>
    /// <returns></returns>
    public Target FreeTarget()
    {
        if (Seats != null)
        {
            return Seats.Find(target => target.IsOccupied == false);
        }
        return null;
    }

    
    

}
