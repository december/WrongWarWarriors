using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour {

    private Animator anim;
    private int randomTime;
    private float lastTime;
    public enum State {Sleep, Shout, Saved};
    public State state = State.Sleep;
    // Use this for initialization
    void Start () {
        randomTime = Random.Range(8, 15);
        lastTime = Time.fixedTime;
        anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Sleep:
                float tempTime = Time.fixedTime;
                if (tempTime - lastTime >= randomTime)
                {
                    lastTime = tempTime;
                    randomTime = Random.Range(8, 15);
                    anim.SetTrigger("WantToShout");
                }
                break;
            case State.Shout:
                break;
            case State.Saved:
                Debug.Log("Clear!");
                break;
        }
    }

    public void GetSaved(){
        state = State.Saved;
        anim.SetTrigger("Saved");
    }
}
