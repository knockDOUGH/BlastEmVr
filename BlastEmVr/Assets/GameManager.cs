using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    Cardboard.SDK.OnTrigger += delegate
	    {
	        Quaternion rotation = Cardboard.SDK.HeadPose.Orientation;
	        Debug.Log("x: " + rotation.x + "");
            Debug.Log("y: " + rotation.y + "");
	    };
	}
	
	// Update is called once per frame
	void Update () {
	}
}
