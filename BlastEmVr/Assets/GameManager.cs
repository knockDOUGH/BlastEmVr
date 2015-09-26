using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    private GameObject _cannonBall;

	// Use this for initialization
	void Start ()
	{
        _cannonBall = GameObject.FindGameObjectWithTag("CannonBall");
        _cannonBall.SetActive(false);

	    Cardboard.SDK.OnTrigger += delegate
	    {
	    };
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void ShootCannonBall()
    {
        Quaternion rotation = Cardboard.SDK.HeadPose.Orientation;
        Debug.Log("x: " + rotation.x + "");
        Debug.Log("y: " + rotation.y + "");

        _cannonBall.SetActive(true);
    }
}
