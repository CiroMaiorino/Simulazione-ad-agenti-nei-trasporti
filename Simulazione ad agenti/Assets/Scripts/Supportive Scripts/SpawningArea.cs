using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningArea : MonoBehaviour
{
 

    public Vector3 size;
    [Range(0,15),SerializeField] private int averagePendulars;
    public int restartSpeed;
    public int AvaragePendulars { get { return averagePendulars; } set => averagePendulars = value; }

    // Start is called before the first frame update


    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Vector3.zero ,size);
       
    }
}
