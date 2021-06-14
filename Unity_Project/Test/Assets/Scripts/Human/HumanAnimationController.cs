using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimationController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void jump()
    {
        //animator.SetTrigger("JumpTrigger");
        animator.Play("Jump");
    }
}
