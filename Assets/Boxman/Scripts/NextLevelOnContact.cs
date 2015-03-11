using UnityEngine;
using System.Collections;

public class NextLevelOnContact: MonoBehaviour {

	void OnCollisionEnter(Collision other){
		if(other.transform.tag == "Player"){
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.transform.tag == "Player"){
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}
}
