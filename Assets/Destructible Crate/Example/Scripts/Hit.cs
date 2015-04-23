using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	public GameObject DestroyedObject;

	public float hitForce = 10f;
	
	void OnCollisionEnter( Collision collision ) {
		print (collision.relativeVelocity.magnitude);

		if( collision.relativeVelocity.magnitude > hitForce) {
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