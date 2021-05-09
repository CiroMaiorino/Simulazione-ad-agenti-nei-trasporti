using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pullman : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isMooving;
    public List<Target> targets;
  
  ///<summary>
  ///Ritorna il primo posto libero disponibile
  ///</summary>
    public Target freeTarget()
    {
        if (targets != null)
        {
            return targets.Find(target => target.isOccupied == false);
        }
        return null;
    }

}
