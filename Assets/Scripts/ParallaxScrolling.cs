using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {

    [SerializeField] private Transform cameraTransform;
    [SerializeField] float parallaxFactor;

    private Vector3 previousCameraPosition;
    private Vector3 deltaCameraPosition;

	void Start ()
    {
        previousCameraPosition = cameraTransform.position;
	}
	
	void Update ()
    {
        deltaCameraPosition = cameraTransform.position - previousCameraPosition;
        Vector3 parallaxPosition = new Vector3(transform.position.x + (deltaCameraPosition.x * parallaxFactor), 
                                               transform.position.y/* + (deltaCameraPosition.y * parallaxFactor)*/, 
                                               transform.position.z);
        transform.position = parallaxPosition;
        previousCameraPosition = cameraTransform.position;
	}


}
