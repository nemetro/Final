using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	private float timer;
	public float switchFloorTimer;
	public int nextLevel;
	private bool startElevator = false;
	
	void Update(){
		if(startElevator){
			timer += Time.deltaTime;
		}
		if(timer >= switchFloorTimer){
			Application.LoadLevel (nextLevel);
		}
			
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player")
			startElevator = true;
	}
}
