using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningArea : MonoBehaviour
{
 

    public Vector3 size;
    [Range(0,15),SerializeField] private int avaragePendolars=0;
    public int restartSpeed;
    public int AvaragePendolars { get => avaragePendolars; set => avaragePendolars = value; }

    // Start is called before the first frame update


    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Vector3.zero ,size);
       
    }
}
