using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCovid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleTrigger()
    {
        Debug.LogError("Alberello");
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.LogError("Allegria");
    }
}
