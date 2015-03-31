using UnityEngine;
using System.Collections;
using InControl;
//NEW ONE
public class WeaponMechanics : MonoBehaviour {

	public GameObject gun;
	public GameObject crowbar;
	public GameObject bulletHole;

	public AudioClip gunShotSound;
	public AudioClip dryFireSound;
	public AudioClip reloadSound;
	
	public InputDevice controller;
	public int gunDamage = 60;
	public float timeBetweenShots = 0.75f;
	public float reloadTime = 2.0f;
	public int maxBulletsInClip = 6;
	public float vertRecoilDistance = 0.1f;
	public float horzRecoilDistance = 0.1f;
	public Camera playerCamera;

	private int bulletsInClip = 6;
	private float attackCooldown;
	private bool justAttacked;
	private bool usingGun;
	private Vector3 gunStartLoc;
	private Quaternion gunStartRot;

	// Use this for initialization
	void Start () {
		attackCooldown = 0;
		usingGun = false;
		justAttacked = false;
		crowbar.GetComponent<Collider>().enabled = false;
		gunStartLoc = gun.transform.localPosition;
		gunStartRot = gun.transform.localRotation;
	}

	// Update is called once per frame
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

//		if(Input.GetKeyDown("q")) {
		if(controller.Action3.WasPressed){
			if(usingGun) {
				usingGun = false;
			} else if(!usingGun) {
				usingGun = true;
			}
		}

		//Attacks
		//TODO can probably make this modular later (class based with just a call to an attack() function.
		//Melee weapon
		if(usingGun == false) {
			if(Input.GetMouseButtonDown (0)) {
				crowbar.GetComponent<Animation>().Play("Take 001");
			}
//			if (controller.LeftTrigger.WasPressed && attackCooldown <= 0) {
//				attackCooldown = 0.8f;
//				crowbar.transform.Rotate(0,90,0);
//				justAttacked = true;
//				crowbar.GetComponent<Collider>().enabled = true;
//			}
//			if(attackCooldown <= 0.5f && justAttacked) {
//				crowbar.transform.Rotate(0,-90,0);
//				justAttacked = false;
//				crowbar.GetComponent<Collider>().enabled = false;
//			}
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
	}

	void FireGun(){
		RaycastHit hitInfo;
		if(Physics.Raycast(gun.transform.position, gun.transform.forward, out hitInfo)){ //if raycast hits something
			GameObject bulletHoleClone = (GameObject) Instantiate(bulletHole, hitInfo.point + hitInfo.normal * 0.001f, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)); //TODO Layer mask
			if(hitInfo.transform.tag != "Wall"){
				bulletHoleClone.GetComponent<Renderer>().enabled = false;
			}

			if (hitInfo.transform.tag == "Enemy"){
				print ("Hit enemy apply damage: " + gunDamage);
				if(hitInfo.transform.name.ToLower().Contains("head")){
					print ("headshot!");
					hitInfo.transform.root.GetComponent<EnemyHealth>().ApplyDamage(2*gunDamage);
				} else {
					print ("bodyshot!");
					hitInfo.transform.root.GetComponent<EnemyHealth>().ApplyDamage(gunDamage);
				}

				hitInfo.transform.root.GetComponent<EnemyHealth>().ApplyForce(hitInfo.rigidbody, 4*gun.transform.forward);
			}
		}
		AnimateGunRecoil ();
	}

	void AnimateGunRecoil(){
		gun.transform.localPosition = gun.transform.localPosition + new Vector3(0, vertRecoilDistance, -1*horzRecoilDistance);
		gun.transform.localRotation = Quaternion.Euler(new Vector3(-50, 0 , 0));
	}
}
