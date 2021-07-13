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
      bus = transform.parent.transform.parent.GetComponent<Bus>();       
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "StopTrigger")
        {
            bus.isMooving = false;
            bus.GetComponent<PathFollower>().speed = 0;
        }
    }
    }
