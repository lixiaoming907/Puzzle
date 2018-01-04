using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDiffcualtAnimation : MonoBehaviour
{
    private bool openOrClose = true;

    void OnEnable()
    {
        OpenThis();
    }

    public void OpenThis()
    {
        openOrClose = true;
    }

    public void CloseThis()
    {
        openOrClose = false;
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (openOrClose)
	    {
	        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 0.15f);
	    }
	    else
	    {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.15f);
        }
	}
}
