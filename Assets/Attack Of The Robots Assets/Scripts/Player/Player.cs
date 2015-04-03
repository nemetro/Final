using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public int health;
	public int num;
	public int MAXHEALTH = 20;
	public Camera cam;
	public WeaponMechanics weaponScript;

	void Start () {
		DontDestroyOnLoad(this);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		health = MAXHEALTH;
		Level.players.Add(this);
		weaponScript = this.GetComponent<WeaponMechanics>();
	}
	
	void Update () {

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
	}
}
