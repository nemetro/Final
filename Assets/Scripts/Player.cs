using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

<<<<<<< HEAD
	public static int health;
	public int MAXHEALTH = 20;
	public Vector3 startPos;
=======
	public int health;
	public int num;
	public int MAXHEALTH = 20;
	public Camera cam;
>>>>>>> master

	void Start () {
		Screen.lockCursor = true;
		health = 0;
<<<<<<< HEAD
=======
		Level.players.Add(this);
>>>>>>> master
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
<<<<<<< HEAD
		if(other.tag == "Blue" || other.tag == "Red"){
			Destroy (other.gameObject);
		}
=======
>>>>>>> master
	}
}
