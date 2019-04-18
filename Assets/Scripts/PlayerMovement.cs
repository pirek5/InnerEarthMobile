using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : Player {

    //config
    [SerializeField] private float movingHorizontalSpeedOnLadder = 1f;
    [SerializeField] private float defaultMovingSpeed = 1f;
    [SerializeField] private float climbingLadderSpeed = 1f;
    [SerializeField] private float pushingCrateSpeed = 1f;
    [SerializeField] private float jumpingStrenght = 1f;
    [SerializeField] private float pullingRange = 1f;

    [SerializeField] private LayerMask crateMask;

    //state
    public float currentMovingSpeed;
    private GameObject crateToPull;

    void Start ()
    {
        currentMovingSpeed = defaultMovingSpeed;
        Player.IsActive = true;
        Physics2D.queriesStartInColliders = false;
    }

    protected override void Update () {
        base.Update();
        if (isActive)
        {
            FlipSide();
            AnimatePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            MovingHorizontal();
            Jumping();
            PullingCrate();
            ClimbingLadder();
            SetMovingSpeed();
        }
    }

    private void MovingHorizontal()
    {
        myRigidbody.velocity = new Vector2(xAxisInput * currentMovingSpeed, myRigidbody.velocity.y);
    }

    private void Jumping()
    {
        if (jumpPressed && isTouchingGround && !isHoldingCrate)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpingStrenght);
            jumpPressed = false;
        }
        
    }

    private void ClimbingLadder()
    {
        if (isTouchingLadder)
        {
            myRigidbody.gravityScale = 0;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, yAxisInput * climbingLadderSpeed);
        }
        else
        {
            myRigidbody.gravityScale = 1;
        }
    }

    private void PullingCrate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale, pullingRange, crateMask);
        if(hit.collider != null && shiftPressed && isTouchingGround)
        {
            isHoldingCrate = true;
            crateToPull = hit.collider.gameObject;
            crateToPull.GetComponent<FixedJoint2D>().enabled = true;
            crateToPull.GetComponent<FixedJoint2D>().connectedBody = myRigidbody;
            crateToPull.GetComponent<Crate>().isMoveable = true;
        }
        else if(!shiftPressed)
        {
            isHoldingCrate = false;
            if (crateToPull)
            {
                crateToPull.GetComponent<FixedJoint2D>().enabled = false;
                crateToPull.GetComponent<Crate>().isMoveable = false;
            }
        }
    }

    private void SetMovingSpeed()
    {
        if (isHoldingCrate)
        {
            currentMovingSpeed = pushingCrateSpeed;
        }
        else if (isTouchingLadder && !isTouchingGround)
        {
              
            currentMovingSpeed = movingHorizontalSpeedOnLadder;
        }
        else
        {
            currentMovingSpeed = defaultMovingSpeed;
        }
    }

    private void FlipSide()
    {
        if (xAxisInput > 0 && !isHoldingCrate)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if(xAxisInput < 0 && !isHoldingCrate)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    private void AnimatePlayer()
    {
        if(Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        {
            animator.SetBool("Running", true);
            animator.speed = Mathf.Abs(xAxisInput);
        }
        else
        {
            animator.SetBool("Running", false);
            animator.speed = 1f;
        }


        //animator.SetBool("Running", Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        animator.SetBool("OnLadder", isTouchingLadder && !isTouchingGround);
        animator.SetBool("Climbing", isTouchingLadder && !isTouchingGround && (yAxisInput != 0 || xAxisInput != 0));

        animator.SetBool("PushingIdle", isHoldingCrate);
        if (isHoldingCrate)
        {
            animator.SetBool("Pushing", xAxisInput * transform.localScale.x > 0);
            animator.SetBool("Pulling", xAxisInput * transform.localScale.x < 0);
        }
        else
        {
            animator.SetBool("Pushing", false);
            animator.SetBool("Pulling", false);
        }
    }
}
