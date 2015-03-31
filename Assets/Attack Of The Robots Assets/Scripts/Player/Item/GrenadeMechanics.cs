using UnityEngine;
using System.Collections;

public class GrenadeMechanics : MonoBehaviour {

	public float radius;
	public float power;
	
	// Use this for initialization
	void Start () {
		radius = 2.0f;
		power = 1000.0f;
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision collision) {

		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
			
		foreach (Collider hit in colliders) {
			if (hit && hit.GetComponent<Rigidbody>()) {
				hit.GetComponent<Rigidbody>().AddExplosionForce (power, transform.position, radius, 3.0f);
			}
		}
		Destroy (gameObject);
	}
}
