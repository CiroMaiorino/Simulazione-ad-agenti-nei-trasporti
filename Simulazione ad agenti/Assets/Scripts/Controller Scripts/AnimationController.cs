using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AnimationController : MonoBehaviour
{
    private Agent agent;
    private AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInParent<Agent>();
        aiPath = GetComponentInParent<AIPath>();
    }

    public void AnimationGetOff()
    {
        Destroy(agent.GetComponent<Rigidbody>());
        aiPath.enabled = true;
    }
    
}
