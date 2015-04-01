using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerShutDown : MonoBehaviour {
	
	public List<GameObject> spawners = new List<GameObject>();
	
	void OnTriggerStay (Collider other)
	{
		// If the colliding gameobject is the player...
		if(other.gameObject.tag == "Player")
			if(Input.GetButton("Switch"))
				foreach(GameObject s in spawners){
					s.GetComponent<Spawner>().on = false;
				}
	}
}
