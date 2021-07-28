using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCovid : MonoBehaviour
{
    private int infectionPercentage;

    public int InfectionPercentage { get => infectionPercentage; set => infectionPercentage = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (Utility<Transform>.Infected(InfectionPercentage))
        {

            GetComponentInParent<Agent>().Infected();
        }
    }
}
