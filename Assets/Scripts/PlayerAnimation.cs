using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
   public Animator animator;
   public int isRunningHash;
    public int isSlidingHash;
    public int isJumpingHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isSlidingHash = Animator.StringToHash("isSliding");
    }

    // Update is called once per frame
    public void SetRunning(bool isRunning)
    {
        animator.SetBool(isRunningHash, isRunning);
    }
    public void Sliding(bool isSliding)
    {
        animator.SetBool(isSlidingHash, isSliding);
    }
    public void Jumping(bool isJumping)
    {
        animator.SetBool(isJumpingHash, isJumping);
    }
}
