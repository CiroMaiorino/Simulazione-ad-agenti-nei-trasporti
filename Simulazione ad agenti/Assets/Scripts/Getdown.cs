using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getdown : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Exiting")
            other.transform.parent.GetComponent<Agent>().Exiting();
    }
}
