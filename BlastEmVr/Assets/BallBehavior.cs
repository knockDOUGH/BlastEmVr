using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {
    private GameObject Enemy;
    public GameObject Explosion;

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
            Debug.Log("came");
            Instantiate(Explosion,
                        transform.position,
                        Quaternion.identity);
            c.gameObject.BroadcastMessage("Hit", c.transform.position);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 3f);
        }

    }
}
