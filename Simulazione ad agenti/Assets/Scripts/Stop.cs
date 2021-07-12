using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    private Bus bus;
    // Start is called before the first frame update
    void Start()
    {
        bus = transform.parent.parent.GetComponent<Bus>();
    }
    private void onTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "StopTrigger")
        {
            Debug.Log("Firmt");
            bus.isMooving = false;
            GetComponent<PathFollower>().speed = 0;
        }
    }
    }
