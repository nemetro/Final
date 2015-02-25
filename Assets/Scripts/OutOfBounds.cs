using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

	public Player player;

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			Player.health = 0;
		}
		else{
			Destroy(other.gameObject);
		}
	}
}
