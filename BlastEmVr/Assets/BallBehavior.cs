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
        if (c.gameObject != null && c.gameObject.transform.parent != null)
        {
            if (c.gameObject.transform.parent.name.Equals("EnemyTank"))
            {
                Debug.Log("came");
                Instantiate(Explosion,
                            transform.position,
                            Quaternion.identity);
                c.gameObject.transform.parent.BroadcastMessage("Hit", c.transform.position);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 3f);

    }
}
