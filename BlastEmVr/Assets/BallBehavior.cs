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

    void OnCollisionEnter(Collision otherObject)
    {
        Debug.Log("Canon ball collision");
        if (otherObject.gameObject != null && otherObject.gameObject.tag.Equals("Explodable"))
        {
            Debug.Log("came");
            Instantiate(Explosion,
                        transform.position,
                        Quaternion.identity);
            otherObject.gameObject.BroadcastMessage("Hit", otherObject);
            Destroy(gameObject);
        }

        if(gameObject != null) Destroy(gameObject, 3f);
    }
}
