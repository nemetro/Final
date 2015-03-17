using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

	public Player player;

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			foreach(Player player in Level.players){
				if(other.gameObject == player.gameObject)
					player.health = 0;
			}
		}
		else{
			Destroy(other.gameObject);
		}
	}
}
