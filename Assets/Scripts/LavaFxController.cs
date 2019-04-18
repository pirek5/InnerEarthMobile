using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LavaFxController : MonoBehaviour {

    // references set in editor
    [SerializeField] private Vector2 leftEdge, rightEdge;
    [SerializeField] private GameObject detectorPrefab;

    //config
    [SerializeField] private float detectorsPerUnit;

    //cached data
    private float distance;
    private float distanceBetweenDetectors;
    private int detectorsQuantity;

    private void Start () {
        DataCalculation();
        InstantinatingDetectors();
	}


    private void DataCalculation()
    {
        distance = Vector2.Distance(leftEdge, rightEdge);
        distanceBetweenDetectors = 1 / detectorsPerUnit;
        detectorsQuantity = (int)distance * (int)detectorsPerUnit;
    }

    private void InstantinatingDetectors()
    {
        Vector2 currentDetectorPosition = leftEdge;
        for (int i = 0; i <= detectorsQuantity; i++)
        {
            GameObject InstantietedDetector = Instantiate(detectorPrefab, currentDetectorPosition, Quaternion.identity);
            InstantietedDetector.transform.parent = gameObject.transform;
            Vector2 nextDetectorPosition = currentDetectorPosition + new Vector2(distanceBetweenDetectors, 0f);
            currentDetectorPosition = nextDetectorPosition;
        }
    }

}
