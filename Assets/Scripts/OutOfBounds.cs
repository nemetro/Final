using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

	public Player player;

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
<<<<<<< HEAD
			Player.health = 0;
=======
			foreach(Player player in Level.players){
				if(other.gameObject == player.gameObject)
					player.health = 0;
			}
>>>>>>> master
		}
		else{
			Destroy(other.gameObject);
		}
	}
}
