using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float wallDetectRange = 1f;
    [SerializeField] private float floorDetectRange = 1f;
    [SerializeField] private float floorDetectOffset = 1f;
    [SerializeField] private LayerMask wallMask;

    private Rigidbody2D myRigidbody;
    bool stopped = false;
    void Awake () {
        myRigidbody = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }
	
	void Update () {
        if (IsTimeToTurn())
        {
            Turn();
        }
	}

    private void FixedUpdate()
    {
        if (stopped) { return; }
        myRigidbody.velocity = new Vector2(movementSpeed, myRigidbody.velocity.y);
    }

    private void Turn()
    {
        transform.localScale = new Vector2(transform.localScale.x * (-1), transform.localScale.y);
        movementSpeed = movementSpeed *(-1f);
    }

    public void Stop()
    {
        stopped = true;
        myRigidbody.velocity = Vector2.zero;
    }

    private bool IsTimeToTurn()
    {
        RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale, wallDetectRange, wallMask);
        Vector2 raycastStartPos = new Vector2(transform.position.x + floorDetectOffset * transform.localScale.x, transform.position.y);
        RaycastHit2D floor = Physics2D.Raycast(raycastStartPos, new Vector2(1f,-1f) * transform.localScale, floorDetectRange);

        if (wall || !floor) //turn if wall or end of floor detected
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
