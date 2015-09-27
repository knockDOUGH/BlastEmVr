using UnityEngine;
using System.Collections;

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

    public void Hit(Vector3 position)
    {
        hitCount++;
        Debug.Log("hit #" + hitCount);
        if (hitCount >= 3)
        {
            Explode(position);
        }
    }

    public void Explode(Vector3 position)
    {
        Instantiate(Explosion,
                    transform.position,
                        Quaternion.identity);
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Collider[] colliders = Physics.OverlapSphere(hit.point, 10);

            foreach (Collider c in colliders)
            {
                Debug.Log("hit #" + hitCount);
                if (c.attachedRigidbody == null)
                {
                    continue;
                }
                Debug.Log("explode");
                c.attachedRigidbody.AddExplosionForce(10 /*force*/, hit.point, 10, 3, ForceMode.Impulse);
            }
            //body.AddExplosionForce(5 /*force*/, hit.point, 5, 3, ForceMode.Impulse);
        }
            //Destroy(gameObject);
    }
}
