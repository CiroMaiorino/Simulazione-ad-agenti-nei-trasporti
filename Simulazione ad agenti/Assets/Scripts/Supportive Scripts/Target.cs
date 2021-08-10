using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class represents the Target. */ 
public class Target : MonoBehaviour
{

    /// <summary>
    ///Parameter that indicates if the seat is Occupied with his get and set methods. 
    /// </summary>
    private bool isOccupied;

    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }
}
