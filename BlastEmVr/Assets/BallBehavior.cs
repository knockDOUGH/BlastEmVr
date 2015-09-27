using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {
    public GameObject Enemy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name.Equals("EnemyTank"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 3f);
        }

    }
}
