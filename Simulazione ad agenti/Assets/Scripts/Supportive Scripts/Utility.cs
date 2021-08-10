using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  Utility<T> where T : Component
{
    /// <summary>
    /// Return a list of childrens component
    /// </summary>
    /// <param name="parent"> </param>
    /// <returns></returns>
    public static List<T> GetAllChildren(GameObject parent) 
    {
        List<T> children = new List<T>();
        foreach (Transform child in parent.transform)
            if(child.GetComponent<T>()!=null)
                children.Add(child.GetComponent<T>());
        return children;
    }
    public static bool Infected(int infectionPercentage)
    {
        return Random.Range(0, 100) < infectionPercentage;
           
        
    }

}
