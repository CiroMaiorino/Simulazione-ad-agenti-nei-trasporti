namespace TurnTheGameOn.Timer
{
    using UnityEngine;
    using System.Collections.Generic;

    public class DestroyObjects : MonoBehaviour
    {
        public List<GameObject> objectsToDestroy;

        public void Destroy()
        {
            for (int i = 0; i < objectsToDestroy.Count; i++)
            {
                Destroy(objectsToDestroy[i]);
            }
            objectsToDestroy.Clear();
        }

    }
}