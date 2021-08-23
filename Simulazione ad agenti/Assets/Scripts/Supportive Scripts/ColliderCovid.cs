using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCovid : MonoBehaviour
{
    [HideInInspector]public int InfectionPercentage;


    private void OnParticleCollision(GameObject other)
    {


        if (Utility<Transform>.Infected(InfectionPercentage))
        {
            if (tag == "Agent")
            {
                GetComponentInParent<Agent>().Infected();
            }
            else
            {

                GetComponent<Agent>().Infected();
            }
        }
    }
}
