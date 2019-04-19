 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour { // base class - collect input and information about state of player

    //set in editor
    [SerializeField] private Joystick joystick;

    //config
    [SerializeField] private float minMovement;
    [SerializeField] private float deadzone;

    //state

    static public bool IsActive { get; set; }
    public float xAxisInput, yAxisInput;
    public bool isTouchingGround, isTouchingEnemy, isHeadTouchingLava, isTouchingWater, isTouchingLava, isTouchingLadder, isHoldingCrate;
    public bool jumpPressed, shiftPressed;
    public bool zeroVelocity;

    //cached components 

    private BoxCollider2D feetCollider;
    private CapsuleCollider2D bodyCollider;
    private CircleCollider2D headCollider;

    void Awake () {
        headCollider = GetComponent<CircleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();

        CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Touch);
	}

    protected void Update ()
    {
        //State
        isTouchingGround = IsPlayerTouching(feetCollider, "Ground") || IsPlayerTouching(feetCollider, "Crates");
        isTouchingLadder = IsPlayerTouching(feetCollider, "Ladder");
        isTouchingLava = IsPlayerTouching(feetCollider, "Lava") || IsPlayerTouching(bodyCollider, "Lava");
        isHeadTouchingLava = IsPlayerTouching(headCollider, "Lava");
        isTouchingWater = IsPlayerTouching(headCollider, "DeepWater");
        isTouchingEnemy = IsPlayerTouching(bodyCollider, "Hazards");
        

        //Input
        if(joystick.Horizontal < deadzone && joystick.Horizontal > -deadzone)
        {
            xAxisInput = 0f;
        }
        else if(joystick.Horizontal < minMovement && joystick.Horizontal > -minMovement)
        {
            xAxisInput = minMovement * Mathf.Sign(joystick.Horizontal);
        }
        else
        {
            xAxisInput = joystick.Horizontal;
        }

        if (joystick.Vertical < deadzone && joystick.Vertical > -deadzone)
        {
            yAxisInput = 0f;
        }
        else if (joystick.Vertical < minMovement && joystick.Vertical > -minMovement)
        {
            yAxisInput = minMovement* Mathf.Sign(joystick.Vertical);
        }
        else
        {
            yAxisInput = joystick.Vertical;
        }


        jumpPressed = CrossPlatformInputManager.GetButtonDown("Jump");
        shiftPressed = CrossPlatformInputManager.GetButton("Fire1");
    }

    protected bool IsPlayerTouching(Collider2D collider, string thing)
    {
        return collider.IsTouchingLayers(LayerMask.GetMask(thing));
    }
}
