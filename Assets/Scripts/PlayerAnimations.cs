using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    Player player;
    Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AnimatePlayer();
    }

    private void AnimatePlayer()
    {
        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        {
            animator.SetBool("Running", true);
            animator.speed = Mathf.Abs(player.xAxisInput);
        }
        else
        {
            animator.SetBool("Running", false);
            animator.speed = 1f;
        }

        animator.SetBool("OnLadder", player.isTouchingLadder && !player.isTouchingGround);
        animator.SetBool("Climbing", player.isTouchingLadder && !player.isTouchingGround && (player.yAxisInput != 0 || player.xAxisInput != 0));

        animator.SetBool("PushingIdle", player.isHoldingCrate);
        if (player.isHoldingCrate)
        {
            animator.SetBool("Pushing", player.xAxisInput * transform.localScale.x > 0);
            animator.SetBool("Pulling", player.xAxisInput * transform.localScale.x < 0);
        }
        else
        {
            animator.SetBool("Pushing", false);
            animator.SetBool("Pulling", false);
        }
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    public void Drowning()
    {
        animator.SetTrigger("Drowning");
    }

    public void Grave()
    {
        animator.SetTrigger("Grave");
    }
}
