using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour {

    public int iconIndex;
    private Animator animator;
    public bool isEnabled = true;

	void Awake ()
    {
        animator = GetComponent<Animator>();
	}

    public void ChangeStatus()
    {
        isEnabled = !isEnabled;
        animator.SetBool("Enabled", isEnabled);
    }	
}
