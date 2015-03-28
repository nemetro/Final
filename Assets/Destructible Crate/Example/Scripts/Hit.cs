using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	public GameObject DestroyedObject;

	public float hitForce = 1f;
	
	void OnCollisionEnter( Collision collision ) {
		if( collision.impactForceSum.magnitude > hitForce) {
		DestroyIt();
		}
	}
	
		void DestroyIt(){
		if(DestroyedObject) {
			Instantiate(DestroyedObject, transform.position, transform.rotation);
		}
		Destroy(gameObject);

	}
}