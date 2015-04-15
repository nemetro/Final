using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int num;

	void Start () {
		DontDestroyOnLoad(this);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Level.players.Add(this);
	}
	
	/*void Update () {

		if (Input.GetKeyUp (KeyCode.BackQuote)) {
			if (Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
			Cursor.visible = !Cursor.visible;
			//Screen.lockCursor = !Screen.lockCursor;
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Kill"){
			health = 0;
		}
	}*/
}
