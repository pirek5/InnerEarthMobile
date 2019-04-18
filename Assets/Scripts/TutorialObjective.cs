using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;

public class TutorialObjective : MonoBehaviour {

    //config
    [SerializeField] private LayerMask typeOfObject; 

    //cached
    Tutorial tutorialControler;
    Collider2D myCollider;

    public void Init()
    {
        tutorialControler = FindObjectOfType<Tutorial>();
        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = true;
    }

    private void Update()
    {
        if (myCollider == null || tutorialControler == null)
        {
            return;
        }

        if (myCollider.IsTouchingLayers(typeOfObject))
        {
            tutorialControler.NextPage();
            myCollider.enabled = false; ;
        }
    }


}
