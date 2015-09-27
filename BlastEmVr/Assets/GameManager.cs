using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    private GameObject _cannonBall;
    private GameObject _playerTankBody;

	// Use this for initialization
	void Start ()
	{
        _cannonBall = GameObject.FindGameObjectWithTag("CannonBall");
	    _playerTankBody = GameObject.FindGameObjectWithTag("PlayerTankTower");

        //_cannonBall.SetActive(false);

	    Cardboard.SDK.OnTrigger += ShootCannonBall;

        ProjectCannonBall();
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

    private void ProjectCannonBall()
    {
        // var heading = target.position - player.position;
        Vector3 projectionVector = _cannonBall.transform.position - _playerTankBody.transform.position;
        _cannonBall.GetComponent<Rigidbody>().AddForce(projectionVector, ForceMode.Impulse);
    }
}
