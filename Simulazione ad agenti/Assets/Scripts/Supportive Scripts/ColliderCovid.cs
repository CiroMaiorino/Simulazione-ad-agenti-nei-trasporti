using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCovid : MonoBehaviour
{
    [HideInInspector]public int InfectionPercentage;


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
