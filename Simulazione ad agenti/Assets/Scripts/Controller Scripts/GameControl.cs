using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using PathCreation.Examples;
using System.Text.RegularExpressions;

public class GameControl : MonoBehaviour
{
    public Bus bus;
    [SerializeField] GameObject busStops;
    [SerializeField] Agent agentPrefab;
    private List<Transform> stops;
    public CSVWriter writer;
    [SerializeField] private bool resetGames;
    [SerializeField, Range(0, 100)] int InfectionPercentage;
    /// <summary>
    /// Percantage of initial contagious agent
    /// </summary>
    [SerializeField, Range(0, 100)] int ContagiousPercentage;
   [SerializeField] int aTot, aCont, aH, aInf;

    public int ATot { get => aTot; set => aTot = value; }
    public int ACont { get => aCont; set => aCont = value; }
    public int AH { get => aH; set => aH = value; }
    public int AInf { get => aInf; set => aInf = value; }


    private void Awake()
    {
        if(resetGames)
         PlayerPrefs.SetInt("TimesLaunched", 0);
        PlayerPrefs.SetInt("TimesLaunched", PlayerPrefs.GetInt("TimesLaunched") + 1);
        
    }

    void Start()
    {
        
      
        stops = Utility<Transform>.GetAllChildren(busStops);
        Stop stop = Utility<Stop>.GetAllChildren(bus.gameObject.transform.Find("Wheels").gameObject)[0];
        SpawningAtStops();
        int timesLaunched = PlayerPrefs.GetInt("TimesLaunched");
        writer = new CSVWriter(this,"stat"+timesLaunched+".csv");
        writer.createFile();
    }

   

    void Update()
    {
        int actualPassengersNumber = Utility<Agent>.GetAllChildren(bus.Passengers).Count;
        if (bus.GetComponent<PathFollower>().speed == 0)
            if ((Utility<Agent>.GetAllChildren(bus.currentStop).Count == 0 || actualPassengersNumber == bus.maxPassengers) && CanStart(Utility<Agent>.GetAllChildren(bus.Passengers)))
                bus.StartEngine();
            else if (bus.Returning && CanStart(Utility<Agent>.GetAllChildren(bus.Passengers)))
                bus.StartEngine();
        TimeInputs();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="passangers"></param>
    /// <returns>Return true if all passangers are sat</returns>
    public bool CanStart(List<Agent> passangers)
    {
        foreach (Agent a in passangers)
        {
            if (a.AnimatorStatus())
                return false;
        }
        return true;
    }


    /// <summary>
    /// Spawn agents at all stop station
    /// </summary>
    public void SpawningAtStops()
    {
        List<SpawningArea> areas = new List<SpawningArea>();
        foreach (Transform stop in stops)
        {
            List<SpawningArea> spawnAreas=Utility<SpawningArea>.GetAllChildren(stop.gameObject);
            areas.Add(spawnAreas[0]);
            areas.Add(spawnAreas[1]);
        }
        foreach (SpawningArea area in areas)
        {
            
            int effectivePendolars = Random.Range(area.AvaragePendolars - area.AvaragePendolars * 30 / 100, area.AvaragePendolars + area.AvaragePendolars * 30 / 100);

            for (int i = 0; i < effectivePendolars; i++)
                SpawnAgent(area);

               }
    }

    /// <summary>
    /// Spawn an agent at spawing area
    /// </summary>
    /// <param name="area"> area where spawn the agent</param>
    private void SpawnAgent(SpawningArea area)
    {
        aTot++;
        Transform stop = area.transform.parent;
        Vector3 position = area.transform.position + new Vector3(Random.Range(-area.size.x / 2, area.size.x / 2),
            0,
            Random.Range(-area.size.z / 2, area.size.z / 2));

        agentPrefab.bus = bus;
        int stopNumber= System.Int32.Parse(Regex.Match(stop.name, @"\d+$").Value);
        agentPrefab.Mystop = ((stopNumber + Random.Range(0, 3)) % stops.Count)+1;
        if (Utility<Transform>.Infected(ContagiousPercentage))
        {
            agentPrefab.State = Agent.States.Contagious;
            aCont++;
        }
        else
        {
            agentPrefab.State = Agent.States.Healthy;
            agentPrefab.GetComponentInChildren<ColliderCovid>().InfectionPercentage = InfectionPercentage;

        }
        Instantiate(agentPrefab.gameObject, position, Quaternion.identity).transform.parent = stop;
    }
   
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Reasume()
    {
        Time.timeScale = 1;
    }
    public void setTime(float k)
    {
        Time.timeScale=k;
    }
    private void TimeInputs()
    {
        if (Input.GetKeyDown(KeyCode.P))
            if (Time.timeScale == 1)
                Pause();
            else
                Reasume();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            setTime(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            setTime(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            setTime(3);
        if (Input.GetKeyDown(KeyCode.Alpha0))
            setTime(0.5f);
    }

    public void addHealthy()
    {
        aH++;
    }
    public void addInfected()
    {
        aInf++;
    }

    public void ResetStats()
    {
        aCont = 0;
        aH = 0;
        aInf = 0;
        aTot = 0;
    }
}
