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
       
        if (transform.parent.gameObject != null)
            Debug.Log("papà è viv");
        else Debug.Log("papà è muort");
        if (transform.parent.parent.gameObject != null)
            Debug.Log("o nonn è viv");
        else Debug.Log("o nonn è muort");
        bus = transform.parent.transform.parent.GetComponent<Bus>();
       
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "StopTrigger")
        {
            Debug.Log("Firmt");
            bus.isMooving = false;
            bus.GetComponent<PathFollower>().speed = 0;
        }
    }
    }
