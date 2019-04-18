using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

    [SerializeField] private float lavaSpeed = 0f;
    public bool isFloating = false;
	
	void Update () {
        if (isFloating)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + lavaSpeed * Time.deltaTime);
        }
	}
}
