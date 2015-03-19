using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (BoxCollider))]

public class CharacterControls : MonoBehaviour {
	
	public float speed = 10.0f;
	public float gravityMagnitude = 10.0f;
	public Vector3 gravity;
	public float maxVelocityChange = 10.0f;
	public float jumpHeight = 2.0f;
	public float jumpTimer = 0.0f;
	public float jumpWait = 0.25f;
	public float movementMultiplierWhenInAir = 1.0f;

	public bool grounded = false;
		
	void Start ()	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
	
	void Awake () {
		GetComponent<Rigidbody>().freezeRotation = true;
		GetComponent<Rigidbody>().useGravity = false;
		gravity = new Vector3 (0, -1, 0); //default down
	}
	
	void FixedUpdate () {
		if (jumpTimer > 0) {
			jumpTimer -= Time.fixedDeltaTime;
		}

		if(Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0){
			GetComponent<Rigidbody>().AddForce (getMovementForce ()*100, ForceMode.Acceleration);
		}

		if (grounded && Input.GetButton ("Jump") && jumpTimer <= 0.0f) {
			GetComponent<Rigidbody>().AddForce(getJumpForce(), ForceMode.Impulse);
			jumpTimer = jumpWait;
		}

		// We apply gravity manually for more tuning control
		GetComponent<Rigidbody>().AddForce (gravity * gravityMagnitude * GetComponent<Rigidbody>().mass, ForceMode.Acceleration);

		grounded = false;
	}

	public Vector3 getMovementForce(){
		// Calculate how fast we should be moving
		Vector3 currentLocalVelocity = transform.InverseTransformDirection (GetComponent<Rigidbody>().velocity); //to local coorinates
		Vector3 localTargetVelocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")); //local coordinates
		localTargetVelocity *= speed;

		if (grounded == false) { //in air
			localTargetVelocity *= movementMultiplierWhenInAir;
		}

		// Apply a force that attempts to reach our target velocity
		Vector3 localVelocityChange = (localTargetVelocity - currentLocalVelocity);
		localVelocityChange.x = Mathf.Clamp (localVelocityChange.x, -maxVelocityChange, maxVelocityChange);
		localVelocityChange.z = Mathf.Clamp (localVelocityChange.z, -maxVelocityChange, maxVelocityChange);
		localVelocityChange.y = 0;Mathf.Clamp (localVelocityChange.y, 0, CalculateJumpSpeed ());
		
		return transform.TransformDirection (localVelocityChange); //to global coordinates
	}

	public Vector3 getJumpForce () {
		return transform.up * CalculateJumpSpeed();
	}

	public float CalculateJumpSpeed(){
		return 10f;
	}

	public void OnCollisionStay(Collision other){
		grounded = true;
	}

	void setGravity(Vector3 newGravity){
		gravity = Vector3.Normalize (newGravity);
	}
}