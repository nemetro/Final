using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	public GameObject DestroyedObject;

	public float hitForce = 10f;
	
	void OnCollisionEnter( Collision collision ) {
		print (collision.impactForceSum.magnitude);

		if( collision.impactForceSum.magnitude > hitForce) {
		DestroyIt();
		}
	}
	
	public void DestroyIt(){
		if(DestroyedObject) {
			Instantiate(DestroyedObject, transform.position, transform.rotation);
		}
		Destroy(gameObject);

	}
}