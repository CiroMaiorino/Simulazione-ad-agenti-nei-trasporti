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
    private List<Target> seats;
    private Target exit;

    /// <summary>
    /// bus ramp
    /// </summary>
    private GameObject ramp;
    public GameObject currentStop;
    [HideInInspector] public float speed=5f;
    private GameObject passengers;
    public Target Exit { get => exit;}
    public List<Target> Seats { get => seats; set => seats = value; }
    public GameObject Passengers { get => passengers; set => passengers = value; }

    private void Start() {
        Seats = Utility<Target>.GetAllChildren(TargetParent);
        exit = Seats[Seats.Count - 1];
        Seats.RemoveAt(Seats.Count - 1);
        
        ramp=GameObject.Find("Ramp");
        ramp.SetActive(false);
        Passengers = GameObject.Find("Passengers");
    }
  
  
    public void StopEngine()
    {
        isMooving = false;
        GetComponent<PathFollower>().speed = 0;
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
        Debug.Log("BrumBrum"); //Viene elaborata più volte perchè viene chiamata più volte startEngine()
        isMooving = true;
        GetComponent<PathFollower>().speed =speed ;
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
