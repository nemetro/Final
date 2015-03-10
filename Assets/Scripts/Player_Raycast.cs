using UnityEngine;
using System.Collections;

public class Player_Raycast : MonoBehaviour {
	public float bulletMass = 10.0f; //10kg bullets lol
	public float bulletSpeed = 300.0f; //300 m/s velocity

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			print ("shooting");
			RaycastHit hitInfo;
			if(Physics.Raycast(transform.position, transform.forward, out hitInfo)){ //if raycast hits something
				if(hitInfo.transform.GetComponent<Enemy>() != null && hitInfo.transform.GetComponent<Rigidbody>() != null){
					Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
					enemy.ResetStunTimer();
					audio.PlayOneShot(enemy.hurtNoise);

					Rigidbody rigidbody = hitInfo.transform.GetComponent<Rigidbody>();
					rigidbody.useGravity = true;
<<<<<<< HEAD
=======
					rigidbody.freezeRotation = false;
>>>>>>> master
					rigidbody.AddForce( bulletMass * bulletSpeed * transform.forward, ForceMode.Impulse);
				}
			}
		}
	}
}
