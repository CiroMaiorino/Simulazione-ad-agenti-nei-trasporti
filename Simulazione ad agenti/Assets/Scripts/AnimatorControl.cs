using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    Animator animator;
    bool sit,waiting;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
            sit = !sit; 

        if(Input.GetKeyDown(KeyCode.K))
            waiting= !waiting;

        animator.SetBool("Sit",sit);
        animator.SetBool("Waiting",waiting);
    }
}
