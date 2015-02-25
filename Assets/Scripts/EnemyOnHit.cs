using UnityEngine;
using System.Collections;

public class EnemyOnHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Red_Bullet" && gameObject.tag == "Red_Enemy") {
			Destroy(gameObject);
			Destroy(other.gameObject);
		} else if(other.gameObject.tag == "Blue_Bullet" && gameObject.tag == "Blue_Enemy") {
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
	}
}
