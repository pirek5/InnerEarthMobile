using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{

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

    //dependencies
    private Rigidbody2D myRigidbody;
    private Player player;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }


    void Start ()
    {
        currentMovingSpeed = defaultMovingSpeed;
        Player.IsActive = true;
        Physics2D.queriesStartInColliders = false;
    }

    private void FixedUpdate()
    {
        if (Player.IsActive)
        {
            MovingHorizontal();
            Jumping();
            PullingCrate();
            ClimbingLadder();
            SetMovingSpeed();
            FlipSide();
        }

        if (player.zeroVelocity) // prevents weird behavior after death or during drowning
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void MovingHorizontal()
    {
        myRigidbody.velocity = new Vector2(player.xAxisInput * currentMovingSpeed, myRigidbody.velocity.y);
    }

    private void Jumping()
    {
        if (player.jumpPressed && player.isTouchingGround && !player.isHoldingCrate)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpingStrenght);
            player.jumpPressed = false;
        }
        
    }

    private void ClimbingLadder()
    {
        if (player.isTouchingLadder)
        {
            myRigidbody.gravityScale = 0;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, player.yAxisInput * climbingLadderSpeed);
        }
        else
        {
            myRigidbody.gravityScale = 1;
        }
    }

    private void PullingCrate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale, pullingRange, crateMask);
        if(hit.collider != null && player.shiftPressed && player.isTouchingGround)
        {
            player.isHoldingCrate = true;
            crateToPull = hit.collider.gameObject;
            crateToPull.GetComponent<FixedJoint2D>().enabled = true;
            crateToPull.GetComponent<FixedJoint2D>().connectedBody = myRigidbody;
            crateToPull.GetComponent<Crate>().isMoveable = true;
        }
        else if(!player.shiftPressed)
        {
            player.isHoldingCrate = false;
            if (crateToPull)
            {
                crateToPull.GetComponent<FixedJoint2D>().enabled = false;
                crateToPull.GetComponent<Crate>().isMoveable = false;
            }
        }
    }

    private void SetMovingSpeed()
    {
        if (player.isHoldingCrate)
        {
            currentMovingSpeed = pushingCrateSpeed;
        }
        else if (player.isTouchingLadder && !player.isTouchingGround)
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
        if (player.xAxisInput > 0 && !player.isHoldingCrate)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if(player.xAxisInput < 0 && !player.isHoldingCrate)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

}
