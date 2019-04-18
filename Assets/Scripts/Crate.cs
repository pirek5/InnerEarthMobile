using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public bool isMoveable;
    public float constantPositionX;

	// Use this for initialization
	void Start () {
        constantPositionX = transform.position.x;
	}

    // Update is called once per frame
    void Update()
    {

        if (isMoveable)
        {
            constantPositionX = transform.position.x;
        }
        else
        {
            transform.position = new Vector2(constantPositionX, transform.position.y);
        }

    }
}
