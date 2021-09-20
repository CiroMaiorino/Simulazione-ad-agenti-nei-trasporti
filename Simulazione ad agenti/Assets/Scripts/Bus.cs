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
    private bool returning;
    public bool waiting;
    public Target Exit { get => exit;}
    public List<Target> Seats { get => seats; set => seats = value; }
    public GameObject Passengers { get => passengers; set => passengers = value; }
    public bool Returning { get => returning; set => returning = value; }

    private void Start() {
        Seats = Utility<Target>.GetAllChildren(TargetParent);
        exit = Seats[Seats.Count - 1];
        Seats.RemoveAt(Seats.Count - 1);
        gridGraph = AstarPath.active.data.gridGraph;     
        ramp=GameObject.Find("Ramp");
        ramp.SetActive(false);
        Passengers = GameObject.Find("Passengers");
        returning = false;
        waiting = false;
    }
  
  
    public void StopEngine()
    {
        waiting = true;
        isMooving = false;
        GetComponent<PathFollower>().speed = 0;
        gridGraph.center = Vector3.Scale(transform.position,new Vector3(1,0,1));
        gridGraph.rotation =transform.localEulerAngles;
        ramp.SetActive(true);
        AstarPath.active.Scan();
    }

    public void StartEngine()
    {
        waiting = false;
        StartCoroutine("StartEngineCuroutine");
        foreach (Agent a in Utility<Agent>.GetAllChildren(Passengers))
        {
            if (a.State == Agent.States.Contagious)
            {
                var particleSpeed1 = a.GetComponentInChildren<Illness>().GetComponentsInChildren<ParticleSystem>()[0].main;
                var particleSpeed2 = a.GetComponentInChildren<Illness>().GetComponentsInChildren<ParticleSystem>()[1].main;
                particleSpeed1.startSpeed = currentStop.GetComponentInChildren<SpawningArea>().restartSpeed;
                particleSpeed2.startSpeed = currentStop.GetComponentInChildren<SpawningArea>().restartSpeed;
            }
        }
    }
    private IEnumerator StartEngineCuroutine()
    {
        yield return new WaitForSeconds(4f);
        isMooving = true;


        GetComponent<PathFollower>().speed =getArea().restartSpeed;
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
    public SpawningArea getArea()
    {
        SpawningArea area;
        if (Returning)
             area = Utility<SpawningArea>.GetAllChildren(currentStop)[1];
        else 
             area = Utility<SpawningArea>.GetAllChildren(currentStop)[0];
        
        return area;
    }
}

