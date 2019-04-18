 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour { // base class - collect input and information about state of player

    //set in editor
    [SerializeField] private Joystick joystick;

    //config
    [SerializeField] private float deadzone;

    //state
    protected int lives = 3;
    static protected bool isActive;
    static public bool IsActive { get { return isActive; }  set { isActive = value; } }
    public static float xAxisInput, yAxisInput;
    protected bool isTouchingGround, isTouchingEnemy, isHeadTouchingLava, isTouchingWater, isTouchingLava, isTouchingLadder, isHoldingCrate;
    protected bool jumpPressed, shiftPressed;

    //cached components 
    protected Animator animator;
    protected Rigidbody2D myRigidbody;
    protected BoxCollider2D feetCollider;
    protected CapsuleCollider2D bodyCollider;
    protected CircleCollider2D headCollider;

    void Awake () {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        headCollider = GetComponent<CircleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();

	}

    protected virtual void Update ()
    {
        //State
        isTouchingGround = IsPlayerTouching(feetCollider, "Ground") || IsPlayerTouching(feetCollider, "Crates");
        isTouchingLadder = IsPlayerTouching(feetCollider, "Ladder");
        isTouchingLava = IsPlayerTouching(feetCollider, "Lava") || IsPlayerTouching(bodyCollider, "Lava");
        isHeadTouchingLava = IsPlayerTouching(headCollider, "Lava");
        isTouchingWater = IsPlayerTouching(headCollider, "DeepWater");
        isTouchingEnemy = IsPlayerTouching(bodyCollider, "Hazards");

        //Input
        xAxisInput = joystick.Horizontal > deadzone || joystick.Horizontal < -deadzone ? joystick.Horizontal : 0f;
        yAxisInput = joystick.Vertical > deadzone || joystick.Vertical < -deadzone ? joystick.Vertical : 0f;


        //xAxisInput = CrossPlatformInputManager.GetAxis("Horizontal");
        //yAxisInput = CrossPlatformInputManager.GetAxisRaw("Vertical");
        jumpPressed = joystick.Vertical > 0.6f ? true : false;

        //if (CrossPlatformInputManager.GetButtonDown("Jump"))
        //{
        //    jumpPressed = CrossPlatformInputManager.GetButtonDown("Jump");
        //}
        shiftPressed = CrossPlatformInputManager.GetButton("Fire1");
    }

    protected bool IsPlayerTouching(Collider2D collider, string thing)
    {
        return collider.IsTouchingLayers(LayerMask.GetMask(thing));
    }
}
