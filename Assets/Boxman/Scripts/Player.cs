using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int health;
	public int num;
	public int MAXHEALTH = 20;
	public Camera cam;

	void Start () {
		Screen.lockCursor = true;
		health = 0;
		Level.players.Add(this);
	}
	
	void Update () {

		if(Input.GetKeyUp(KeyCode.BackQuote)){
			Screen.lockCursor = !Screen.lockCursor;
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Kill"){
			health = 0;
		}
	}
}
