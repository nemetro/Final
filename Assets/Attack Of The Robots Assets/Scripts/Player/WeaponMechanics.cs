using UnityEngine;
using System.Collections;
using InControl;

public class WeaponMechanics : MonoBehaviour {

	public GameObject gun;
	public GameObject machete;
	public GameObject blueBullet;
	public bool usingGun;
	public float attackCooldown;
	public bool justAttacked;
	public InputDevice controller;
	

	// Use this for initialization
	void Start () {
		attackCooldown = 0;
		usingGun = false;
		justAttacked = false;
		machete.collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		attackCooldown -= Time.deltaTime;

		if(usingGun) {
			machete.SetActive(false);
			gun.SetActive(true);
		} else {
			gun.SetActive(false);
			machete.SetActive(true);
		}

		if(Input.GetKeyDown ("1")) {
			usingGun = true;
		} 
		if(Input.GetKeyDown("2")) {
			usingGun = false;
		}

//		if(Input.GetKeyDown("q")) {
		if(controller.Action3.WasPressed){
			if(usingGun) {
				usingGun = false;
			} else if(!usingGun) {
				usingGun = true;
			}
		}

		//Attacks

		//Melee weapon
		if(usingGun == false) {
			if (controller.LeftTrigger.WasPressed && attackCooldown <= 0) {
				attackCooldown = .8f;
				machete.transform.Rotate(0,90,0);
				justAttacked = true;
				machete.collider.enabled = true;
			}
			if(attackCooldown <= .5f && justAttacked) {
				machete.transform.Rotate(0,-90,0);
				justAttacked = false;
				machete.collider.enabled = false;
			}
		}

		//gun
		if(usingGun) {
			GameObject bullet = null;

			if(controller.LeftTrigger.WasPressed){ 
				Vector3 pos = gun.transform.position + gun.transform.forward;
				pos = new Vector3(pos.x, pos.y, pos.z);
				bullet = (GameObject)Instantiate(blueBullet, pos, transform.rotation);
				bullet.rigidbody.AddForce(gun.transform.forward*40, ForceMode.VelocityChange);

			}
		}


	}
}
