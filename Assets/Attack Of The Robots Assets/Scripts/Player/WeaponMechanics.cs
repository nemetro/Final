using UnityEngine;
using System.Collections;
using InControl;

public class WeaponMechanics : MonoBehaviour {

	public GameObject gun;
	public GameObject crowbar;
	public GameObject bulletHole;
	public GameObject grenade;
	public Camera playerCamera;

	public AudioClip gunShotSound;
	public AudioClip dryFireSound;
	public AudioClip reloadSound;
	public AudioClip crowbarSwingSound;
	public AudioClip crowbarHitSound;
	
	public InputDevice controller;
	public int gunDamage = 60;
	public float timeBetweenShots = 0.75f;
	public float timeBetweenSwings = 0.25f;
	public float reloadTime = 2.0f;
	public int maxBulletsInClip = 6;
	public int maxBullets = 18;
	public int maxGnades = 4;
	public int numBullets = 18;
	public float vertRecoilDistance = 0.1f;
	public float horzRecoilDistance = 0.1f;
	public int numGnades = 3;
	public float throwForce = 12f;
	public float crowbarRange = 2f;
	public int bulletsInClip = 6;
	public int crowbarDamage = 40;

	private float attackCooldown;
	private bool justAttacked;
	private bool usingGun;
	private Vector3 gunStartLoc;
	private Quaternion gunStartRot;
	private Vector3 crowbarStartLoc;
	private Quaternion crowbarStartRot;

	// Use this for initialization
	void Start () {
		attackCooldown = 0;
		usingGun = false;
		justAttacked = false;
		crowbar.GetComponent<Collider>().enabled = false;
		gunStartLoc = gun.transform.localPosition;
		gunStartRot = gun.transform.localRotation;
		crowbarStartLoc = crowbar.transform.localPosition;
		crowbarStartRot = crowbar.transform.localRotation;
	}
	
	void FixedUpdate () {
	
		if (controller == null)
		{
			controller = InputManager.Devices[0];
		}
		attackCooldown -= Time.deltaTime;

		if(usingGun) {
			crowbar.SetActive(false);
			gun.SetActive(true);
		} else {
			gun.SetActive(false);
			crowbar.SetActive(true);
		}

		if(Input.GetKeyDown ("1")) {
			usingGun = true;
		} 
		if(Input.GetKeyDown("2")) {
			usingGun = false;
		}
		
		if (Input.GetKeyDown ("4") && numGnades > 0) {
			GameObject createGrenade = null;
			print("grenade");
			print (transform.position);
			Vector3 Holder = new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z);
			createGrenade = (GameObject)Instantiate(grenade, Holder + transform.forward, transform.rotation);
			createGrenade.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward+transform.up*.6f)* throwForce, ForceMode.VelocityChange);
			numGnades--;
		}

		if(controller.Action3.WasPressed){ //switch weapons
			if(usingGun) {
				usingGun = false;
			} else if(!usingGun) {
				usingGun = true;
			}
		}

		//Attacks
		//TODO can probably make this modular later (class based with just a call to an attack() function.
		//Melee weapon
		if(!usingGun) {
			if(controller.LeftTrigger.WasPressed && attackCooldown <= 0){
				print ("attack with crowbar");
				attackCooldown = timeBetweenSwings;
				SwingCrowbar();
			}
		}

		//gun
		if(usingGun) {
			if(controller.LeftTrigger.WasPressed && attackCooldown <= 0 && bulletsInClip > 0){
				attackCooldown = timeBetweenShots;
				bulletsInClip--;
				GetComponent<AudioSource>().PlayOneShot(gunShotSound);

				FireGun();
			} else if (controller.LeftTrigger.WasPressed && attackCooldown <= 0 && bulletsInClip <= 0){
				GetComponent<AudioSource>().PlayOneShot(dryFireSound);
			}
			//TODO make this use controller
			if(bulletsInClip < maxBulletsInClip && attackCooldown <= 0 && Input.GetKeyDown(KeyCode.T)){
				attackCooldown = reloadTime;
				GetComponent<AudioSource>().PlayOneShot(reloadSound);
				bulletsInClip = maxBulletsInClip;
			}
		}

		if (gun.transform.localPosition != gunStartLoc) {
			gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition, gunStartLoc, 5 * Time.fixedDeltaTime); 
		}
		
		if (gun.transform.localRotation != gunStartRot) {
			gun.transform.localRotation = Quaternion.Lerp(gun.transform.localRotation, gunStartRot, 5 * Time.fixedDeltaTime);
		}

		if (crowbar.transform.localPosition != crowbarStartLoc) {
			crowbar.transform.localPosition = Vector3.Lerp(crowbar.transform.localPosition, crowbarStartLoc, 5 * Time.fixedDeltaTime); 
		}
		
		if (crowbar.transform.localRotation != crowbarStartRot) {
			crowbar.transform.localRotation = Quaternion.Lerp(crowbar.transform.localRotation, crowbarStartRot, 5 * Time.fixedDeltaTime);
		}
	}

	void FireGun(){
		RaycastHit hitInfo;
		if(Physics.Raycast(gun.transform.position, gun.transform.forward, out hitInfo)){ //if raycast hits something
			GameObject bulletHoleClone = (GameObject) Instantiate(bulletHole, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)); //TODO Layer mask
			if(hitInfo.transform.tag != "Wall"){ //if don't hit a wall then turn off the bullet hole
				bulletHoleClone.GetComponent<Renderer>().enabled = false;
			}

			if (hitInfo.transform.tag == "Enemy"){ //apply damage
				print ("Hit enemy apply damage: " + gunDamage);
				if(hitInfo.transform.name.ToLower().Contains("head")){
					print ("headshot!");
					hitInfo.transform.root.GetComponent<EnemyHealth>().ApplyDamage(2*gunDamage);
				} else if(hitInfo.transform.name.ToLower().Contains("leg") || hitInfo.transform.name.ToLower().Contains("arm")){
					print ("limbshot!");
					hitInfo.transform.root.GetComponent<EnemyHealth>().ApplyDamage(gunDamage/2);
				} else {
					print ("bodyshot!");
					hitInfo.transform.root.GetComponent<EnemyHealth>().ApplyDamage(gunDamage);
				}

				hitInfo.transform.root.GetComponent<EnemyHealth>().ApplyForce(hitInfo.rigidbody, 4*gun.transform.forward);
			}
		}
		AnimateGunRecoil ();
	}

	void SwingCrowbar(){
		RaycastHit hitInfo;
		Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward*crowbarRange, Color.white, 3f);
		LayerMask hittableLayer = LayerMask.GetMask ("Enemy", "Environment", "PlayArea");
		if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, crowbarRange, hittableLayer)) { //if raycast hits something
			GetComponent<AudioSource>().PlayOneShot(crowbarHitSound);

			GameObject bulletHoleClone = (GameObject)Instantiate (bulletHole, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation (Vector3.up, hitInfo.normal)); //TODO Layer mask
			if (hitInfo.transform.tag != "Wall") {
				bulletHoleClone.GetComponent<Renderer> ().enabled = false;
			}
			
			if (hitInfo.transform.tag == "Enemy") {
				print ("hit enemy");
				EnemyHealth enemyHealth = hitInfo.transform.root.GetComponent<EnemyHealth> ();

				if (hitInfo.transform.name.ToLower ().Contains ("head")) {
					print ("headshot!");
					enemyHealth.ApplyDamage (2 * crowbarDamage);
				} else if (hitInfo.transform.name.ToLower ().Contains ("leg") || hitInfo.transform.name.ToLower ().Contains ("arm")) {
					print ("limbshot!");
					enemyHealth.ApplyDamage (crowbarDamage / 2);
				} else {
					print ("bodyshot!");
					enemyHealth.ApplyDamage (crowbarDamage);
				}
				hitInfo.transform.root.GetComponent<EnemyHealth> ().ApplyForce (hitInfo.rigidbody, 4 * gun.transform.forward);
			} else {
				print ("did not hit enemy: " + hitInfo.transform.tag);
			}
		} else {
			print ("missed with crowbar");
			GetComponent<AudioSource>().PlayOneShot(crowbarSwingSound);
		}
		AnimateCrowbarRecoil ();
	}

	void AnimateGunRecoil(){
		gun.transform.localPosition = gun.transform.localPosition + new Vector3(0, vertRecoilDistance, -1*horzRecoilDistance);
		gun.transform.localRotation = Quaternion.Euler(new Vector3(-50, 0 , 0));
	}

	void AnimateCrowbarRecoil(){
		crowbar.transform.localPosition = crowbar.transform.localPosition + new Vector3(0, 0, horzRecoilDistance);
		crowbar.transform.localRotation = Quaternion.Euler(new Vector3(50, 50 , 0));
	}
}
