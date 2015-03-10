using UnityEngine;
using System.Collections;

public class GunMechanics : MonoBehaviour {


	public GameObject blueBullet;
	public GameObject redBullet;
	public float bulletSpeed;
<<<<<<< HEAD
	public bool usingGun;
=======

>>>>>>> master

	public int currentColor;
	//public GameObject redBullet;

	// Use this for initialization
	void Start () {
		renderer.material.SetColor("_Color", Color.blue);
		currentColor = 0;
<<<<<<< HEAD
		usingGun = false;
=======
>>>>>>> master
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("q")) {
			if(currentColor == 0) {
				renderer.material.SetColor("_Color", Color.red);
				currentColor = 1;
			} else {
				renderer.material.SetColor("_Color", Color.blue);
				currentColor = 0;
			}
		}



		if (Input.GetMouseButtonDown(0)) {
			GameObject bullet = null;

			if(currentColor == 0) {
				//GameObject bullet = (GameObject)Instantiate(blueBullet, transform.position + transform.forward, transform.rotation);

				bullet = (GameObject)Instantiate(blueBullet, transform.position + transform.forward, transform.rotation);
				//bullet.rigidbody.AddForce(transform.forward * 100f, ForceMode.Impulse);

			} else if(currentColor == 1) {

				//GameObject bullet = (GameObject)Instantiate(redBullet, transform.position + transform.forward, transform.rotation);
				//GameObject bullet = (GameObject)Instantiate(redBullet, pos, Quaternion.identity);
				//bullet.rigidbody.AddForce(transform.forward * 100f, ForceMode.Impulse);
				bullet = (GameObject)Instantiate(redBullet, transform.position + transform.forward, transform.rotation);
				//bullet.rigidbody.AddForce(transform.forward * 100f, ForceMode.Impulse);



			}
			bullet.rigidbody.AddForce(transform.forward*bulletSpeed, ForceMode.VelocityChange);

			/*
			Vector3 pos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + .5f);
			
			GameObject obj = (GameObject)Instantiate(blueBullet, pos, new Quaternion());
			obj.rigidbody.AddForce(Vector3.forward * 100, ForceMode.Impulse);*/
		}
	}
}
