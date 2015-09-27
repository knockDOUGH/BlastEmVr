using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankEventTracker : MonoBehaviour {

    private int hitCount;
    public GameObject Explosion;
	// Use this for initialization
	void Start () {
        hitCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Hit(Collision tankPart)
    {
        hitCount++;
        Debug.Log("hit #" + hitCount);
        if (hitCount >= 3)
        {
            // get the tank and remove rigidbody
            Destroy(gameObject.GetComponent<Rigidbody>());

            //Add rigid bodies to tank parts
            foreach (Transform part in gameObject.transform)
            {
                Debug.Log("don't be a child");
                part.gameObject.AddComponent<Rigidbody>();
            }

            Explode(tankPart.transform.position);
        }
    }

    public void Explode(Vector3 position)
    {
        Instantiate(Explosion,
                    position,
                        Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(position, 10);

        foreach (Collider c in colliders)
        {
            if (c.attachedRigidbody == null)
            {
                continue;
            }
            Debug.Log("explode");
            c.attachedRigidbody.AddExplosionForce(5 /*force*/, position, 5, 1, ForceMode.Impulse);
        }
    }
}
