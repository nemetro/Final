using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class WeaponMechanics : MonoBehaviour {

	public GameObject gun;
	public GameObject crowbar;
	public GameObject bulletHole;
	public GameObject grenade;
	public Camera playerCamera;

	public AudioSource weapSnd;
	public AudioClip gunShotSound;
	public AudioClip dryFireSound;
	public AudioClip reloadSound;
	public AudioClip crowbarSwingSound;
	public AudioClip crowbarHitSound;
	
	public InputDevice controller;
	public float gunDamage = 60;
	public float timeBetweenShots = 1.2f;
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
	public float weaponSwitchTime = 0.05f;
	public float gunStabilizeSpeed = 10f;
	public float crowbarStabilizeSpeed = 10f;

	private float attackCooldown;
	private float meleeAttackCooldown;
	private bool usingGun;
	private Vector3 gunStartLoc;
	private Quaternion gunStartRot;
	private Vector3 crowbarStartLoc;
	private Quaternion crowbarStartRot;
	private Text onScreen;

	void Awake(){
		onScreen = GameObject.Find ("Hints").GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		attackCooldown = 0;
		meleeAttackCooldown = 0;
		usingGun = false;
		crowbar.GetComponent<Collider>().enabled = false;
		gunStartLoc = gun.transform.localPosition;
		gunStartRot = gun.transform.localRotation;
		crowbarStartLoc = crowbar.transform.localPosition;
		crowbarStartRot = crowbar.transform.localRotation;

		if (controller == null)
		{
			controller = InputManager.Devices[0];
		}
	}

	void Update () {
		attackCooldown -= Time.deltaTime;
		meleeAttackCooldown -= Time.deltaTime;

		if(usingGun) {
			crowbar.SetActive(false);
			gun.SetActive(true);
		} else {
			gun.SetActive(false);
			crowbar.SetActive(true);
		}

		if (controller.LeftTrigger.WasPressed && numGnades > 0) {
			GameObject createGrenade = null;
			print("grenade");
			print (transform.position);
			Vector3 Holder = new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z);
			createGrenade = (GameObject)Instantiate(grenade, Holder + transform.forward, transform.rotation);
			createGrenade.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward+transform.up*.6f)* throwForce, ForceMode.VelocityChange);
			numGnades--;
		}


		bool switchWeapons = controller.Action4.WasPressed;
		if(switchWeapons){ //switch weapons
			if(usingGun) {
				usingGun = false;
			} else if(!usingGun) {
				usingGun = true;
			}
		}

		//Open door
		LayerMask environmentMask = LayerMask.GetMask ("Environment");
		RaycastHit interactableHit;

		bool interactKeyDown = controller.Action1.WasPressed;

		if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out interactableHit, 2.0f, environmentMask)) {
			if(interactableHit.transform.tag == InGameTags.door){
				onScreen.text = "Press 'E' To Use Door";
				if (interactKeyDown) {
					interactableHit.transform.GetComponent<OpenDoor> ().ToggleTheDoor ();
				}
			} else if (interactableHit.transform.tag == InGameTags.deactivateSwitch) { 
				onScreen.text = "Press 'E' To Deactivate Laser Door";
				if (interactKeyDown) {
					GameObject player = this.gameObject;
					interactableHit.transform.GetComponent<LaserDoorSwitchDeactivate>().TurnOffLaserDoor (player);
				}
			}
		} else {
			onScreen.text = "";
		}

		//Attacks
		//TODO can probably make this modular later (class based with just a call to an attack() function.
		//Melee weapon
		if(!usingGun) {
			if(controller.RightTrigger.WasPressed && meleeAttackCooldown <= 0){
				print ("attack with crowbar");
				meleeAttackCooldown = timeBetweenSwings;
				SwingCrowbar();
			}
		}

		//gun
		if(usingGun) {
			if(controller.RightTrigger.WasPressed && attackCooldown <= 0 && bulletsInClip > 0){
				attackCooldown = timeBetweenShots;
				bulletsInClip--;
				weapSnd.PlayOneShot(gunShotSound);

				FireGun();
			} else if (controller.RightTrigger.WasPressed && attackCooldown <= 0 && bulletsInClip <= 0){
				weapSnd.PlayOneShot(dryFireSound);
			}
			//Reload gun
			if(bulletsInClip < maxBulletsInClip && numBullets != 0 && attackCooldown <= 0 && controller.Action3.WasPressed){
				attackCooldown = reloadTime;
				weapSnd.PlayOneShot(reloadSound);
				bulletsInClip = maxBulletsInClip;
				if(numBullets >= maxBulletsInClip){
					numBullets -= maxBulletsInClip;
				} else {
					bulletsInClip = numBullets;
					numBullets = 0;
				}
				AnimateGunReload();
			}
		}
	}

	void FixedUpdate(){
		if (gun.transform.localPosition != gunStartLoc) {
			gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition, gunStartLoc, gunStabilizeSpeed * Time.fixedDeltaTime); 
		}
		
		if (gun.transform.localRotation != gunStartRot) {
			gun.transform.localRotation = Quaternion.Lerp(gun.transform.localRotation, gunStartRot, gunStabilizeSpeed * Time.fixedDeltaTime);
		}
		
		if (crowbar.transform.localPosition != crowbarStartLoc) {
			crowbar.transform.localPosition = Vector3.Lerp(crowbar.transform.localPosition, crowbarStartLoc, crowbarStabilizeSpeed * Time.fixedDeltaTime); 
		}
		
		if (crowbar.transform.localRotation != crowbarStartRot) {
			crowbar.transform.localRotation = Quaternion.Lerp(crowbar.transform.localRotation, crowbarStartRot, crowbarStabilizeSpeed * Time.fixedDeltaTime);
		}
	}

	void FireGun(){
		RaycastHit hitInfo;
		if(Physics.Raycast(gun.transform.position, gun.transform.forward, out hitInfo)){ //if raycast hits something
			GameObject bulletHoleClone = (GameObject) Instantiate(bulletHole, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)); //TODO Layer mask
			if(hitInfo.transform.tag != "Wall"){ //if don't hit a wall then turn off the bullet hole
				bulletHoleClone.GetComponent<Renderer>().enabled = false;
			}

			if(hitInfo.collider.gameObject.GetComponent<Hit>() != null) {
				hitInfo.collider.gameObject.GetComponent<Hit>().DestroyIt();
			}

			if (hitInfo.transform.tag == "Enemy"){ //apply damage
				print ("Hit enemy apply damage: " + gunDamage);
				EnemyHealth enemyHealth = hitInfo.transform.root.GetComponent<EnemyHealth>();

				if(hitInfo.transform.name.ToLower().Contains("head")){
					print ("headshot!");
					enemyHealth.BulletDamage(2*gunDamage);
				} else if(hitInfo.transform.name.ToLower().Contains("leg") || hitInfo.transform.name.ToLower().Contains("arm")){
					print ("limbshot!");
					enemyHealth.BulletDamage(gunDamage/2);
				} else {
					print ("bodyshot!");
					enemyHealth.BulletDamage(gunDamage);
				}

				enemyHealth.ApplyForce(hitInfo.rigidbody, 4*gun.transform.forward);
			}
		}
		AnimateGunRecoil ();
	}

	void SwingCrowbar(){
		RaycastHit hitInfo;
		Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward*crowbarRange, Color.white, 3f);
		LayerMask hittableLayer = LayerMask.GetMask ("Enemy", "Environment", "PlayArea");
		if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, crowbarRange, hittableLayer)) { //if raycast hits something
			weapSnd.PlayOneShot(crowbarHitSound);

			if(hitInfo.collider.gameObject.GetComponent<Hit>() != null) {
				hitInfo.collider.gameObject.GetComponent<Hit>().DestroyIt();
			}

			GameObject bulletHoleClone = (GameObject)Instantiate (bulletHole, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation (Vector3.up, hitInfo.normal)); //TODO Layer mask
			if (hitInfo.transform.tag != "Wall") {
				bulletHoleClone.GetComponent<Renderer> ().enabled = false;
			}
			
			if (hitInfo.transform.tag == "Enemy") {
//				print ("hit enemy");
				EnemyHealth enemyHealth = hitInfo.transform.root.GetComponent<EnemyHealth> ();

				if (hitInfo.transform.name.ToLower ().Contains ("head")) {
//					print ("headshot!");
					enemyHealth.BulletDamage (2 * crowbarDamage);
				} else if (hitInfo.transform.name.ToLower ().Contains ("leg") || hitInfo.transform.name.ToLower ().Contains ("arm")) {
//					print ("limbshot!");
					enemyHealth.BulletDamage (crowbarDamage / 2);
				} else {
//					print ("bodyshot!");
					enemyHealth.BulletDamage (crowbarDamage);
				}
				hitInfo.transform.root.GetComponent<EnemyHealth> ().ApplyForce (hitInfo.rigidbody, 4 * gun.transform.forward);
			} else {
				print ("did not hit enemy: " + hitInfo.transform.tag);
			}
		} else {
			weapSnd.PlayOneShot(crowbarSwingSound);
		}
		AnimateCrowbarRecoil ();
	}

	void AnimateGunRecoil(){
		gun.transform.localPosition = gun.transform.localPosition + new Vector3(0, vertRecoilDistance, -1*horzRecoilDistance);
		gun.transform.localRotation = Quaternion.Euler(new Vector3(-50, 0 , 0));
	}

	void AnimateGunReload(){
		gun.transform.localPosition = gun.transform.localPosition + new Vector3(0, -vertRecoilDistance, horzRecoilDistance);
		gun.transform.localRotation = Quaternion.Euler(new Vector3(50, 0 , 0));
	}

	void AnimateCrowbarRecoil(){
		crowbar.transform.localPosition = crowbar.transform.localPosition + new Vector3(0, 0, horzRecoilDistance);
		crowbar.transform.localRotation = Quaternion.Euler(new Vector3(50, 50 , 0));
	}
}
