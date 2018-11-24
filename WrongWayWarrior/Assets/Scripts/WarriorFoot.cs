﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorFoot : MonoBehaviour {

    public int groundCnt = 0;
    public GameObject StepOn = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StepOn = collision.gameObject;

        if (gameObject.tag == "WarriorFoot" && StepOn.tag == "Princess")
        {
            Princess pr = StepOn.GetComponent<Princess>();
            Warrior wr = gameObject.transform.parent.GetComponent<Warrior>();
            pr.GetSaved();
            wr.FindPrincess();
            Debug.Log("Meet");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        StepOn = collision.gameObject;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag == "Boar" && collision.gameObject.tag == "Boar")
            return;
        if (collision.gameObject == StepOn)
            StepOn = null;
    }

}
