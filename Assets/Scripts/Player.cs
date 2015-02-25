using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static int health;
	public int MAXHEALTH = 20;
	public Vector3 startPos;

	void Start () {
		Screen.lockCursor = true;
		health = 0;
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
		if(other.tag == "Blue" || other.tag == "Red"){
			Destroy (other.gameObject);
		}
	}
}
