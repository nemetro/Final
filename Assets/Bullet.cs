using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float lifeTime = 1.0f;

	void OnCollisionEnter(Collision other){
		rigidbody.useGravity = true;

		if (other.transform.tag == "Enemy") {
			GameObject.Destroy(other.gameObject);
		}
	}

	void Update(){
		if (rigidbody.useGravity) {
			lifeTime -= Time.deltaTime;
		}

		if (lifeTime < 0) {
			GameObject.Destroy(this.gameObject);
		}
	}
}
