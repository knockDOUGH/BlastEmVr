using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
        // destroy the rocket
        // note:
        //  Destroy(this) would just destroy the rocket
        //                script attached to it
        //  Destroy(gameObject) destroys the whole thing
        //Destroy(gameObject);
    }
}
