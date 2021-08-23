using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prova : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Exiting")
            other.transform.parent.GetComponent<Agent>().Exiting();
    }
}
