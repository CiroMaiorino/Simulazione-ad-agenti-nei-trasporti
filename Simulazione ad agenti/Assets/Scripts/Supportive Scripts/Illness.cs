using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illness : MonoBehaviour
{
    private static int emissionAngle;

    public static int EmissionAngle { get => emissionAngle; set => emissionAngle = value; }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RandomRotation", 4f, 3f);
 
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RandomRotation()
    {
        gameObject.transform.localEulerAngles = new Vector3(0, Random.Range(-emissionAngle/2, emissionAngle/2), 0);

    }
}
