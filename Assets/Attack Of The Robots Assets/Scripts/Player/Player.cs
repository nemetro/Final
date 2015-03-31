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
		Screen.lockCursor = true;
		health = MAXHEALTH;
		Level.players.Add(this);
		weaponScript = this.GetComponent<WeaponMechanics>();
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
