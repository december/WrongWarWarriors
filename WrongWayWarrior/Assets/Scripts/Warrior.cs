﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour {

    WarriorFoot Foot;
    public enum State { Step, Air, Stun };
    public State state = State.Step;
    public float stunTime = 0F;
    float moveSpeed = 2.5F, jumpSpeed = 3F;
    float airTime = 0F;
    public bool reversed = false;
    public Vector2 vel = Vector2.zero;
	// Use this for initialization
	void Start () {
        Foot = GetComponentInChildren<WarriorFoot>();
	}

    public Vector2 CalcVel()
    {
        if (Foot.StepOn)
        {
            if (Foot.StepOn.tag == "Boar")
                return Foot.StepOn.GetComponent<Boar>().CalcVel() + vel;
            if (Foot.StepOn.tag == "Bird")
                return Foot.StepOn.GetComponent<Bird>().CalcVel() + vel;
        }
        return vel;
    }

    void Move()
    {
        if (stunTime > 0F)
        {
            state = State.Stun;
            stunTime -= Time.deltaTime;
        }
        else
            if (Foot.StepOn)
            {
                state = State.Step;
            }
            else
            {
                if (stunTime <= 0F)
                    state = State.Air;
            }

        var Body = GetComponent<Rigidbody2D>();
        airTime += Time.deltaTime;
        switch (state)
        {
            case State.Step:
                if (!reversed)
                {
                    if (Input.GetKey(KeyCode.D))
                        vel.x = moveSpeed;
                    else
                        vel.x = 0F;
                }
                else
                {
                    if (Input.GetKey(KeyCode.A))
                        vel.x = -moveSpeed;
                    else
                        vel.x = 0F;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.position += Vector3.up * 0.1F;
                    if (Foot.StepOn && Foot.StepOn.tag == "Boar")
                        vel.y = jumpSpeed * 1.3F;
                    else
                        vel.y = jumpSpeed;
                    airTime = 0F;
                }
                else
                    if (vel.y < 0)
                    vel.y = 0F;
                break;
            case State.Air:
                if(airTime <=0.25F && Input.GetKey(KeyCode.W))
                    vel.y += 12F * Time.deltaTime;

                vel.y -= 8F * Time.deltaTime;
                if (!reversed)
                {
                    if (Input.GetKey(KeyCode.D))
                        vel.x = moveSpeed;
                    else
                        vel.x = 0F;
                }
                else
                {
                    if (Input.GetKey(KeyCode.A))
                        vel.x = -moveSpeed;
                    else
                        vel.x = 0F;
                }
                break;
            case State.Stun:
                vel.y -= 8F * Time.deltaTime;
                break;
        }
        GetComponent<Rigidbody2D>().velocity = CalcVel();
        
        //transform.position += (Vector3)vel * Time.deltaTime;
    }

	// Update is called once per frame
	void Update () {
        Move();

        if (reversed)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.identity;

        /*
        if (state == State.Stun)
            GetComponent<BoxCollider2D>().isTrigger = true;
        else
            GetComponent<BoxCollider2D>().isTrigger = false;
            */
    }
}
