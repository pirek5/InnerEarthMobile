using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCollider : MonoBehaviour {

    public bool isTriggerd = false;
    public Rigidbody2D collisionRigidbody2D;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            collisionRigidbody2D = collision.GetComponent<Rigidbody2D>();
            isTriggerd = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            collisionRigidbody2D = null;
            isTriggerd = false;
        }
    }
}
