using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class Enemy : MonoBehaviour {
	public  enum color {red, blue, purple};
	public float attackRange; //make blue enemies raycast to find player rather than having range
	public float movementSpeed;
	public float maxStunTime = 1.0f; //one second
	public int damage = 2;
	public AudioClip hurtNoise;
	public AudioClip damagePlayerNoise;
	public float movementForce = 45f;

	protected float stunTimer = 0.0f;
	protected color enemyColor;
	protected GameObject playerGO; //TODO will need to change to array for multiplayer
	protected bool touchingObject = false;


	protected virtual void Start(){
		playerGO = GameObject.FindGameObjectWithTag ("Player");
	}

	protected virtual void Update(){
		//enemy fell off platform
		if(transform.position.y < 4){
			Destroy(this.gameObject);
		}

		//can't update if the enemy is stunned
		if (IsStunned ()) {
			stunTimer -= Time.deltaTime;
			return;
		}

		if (PlayerInRange()) {
			if (IsOnGround ()) {
				MoveOnGround ();
			} else {
				MoveInAir ();
			}
		} else {
			rigidbody.velocity = Vector3.zero;
		}

		touchingObject = false;
	}

	protected void OnCollisionEnter(Collision other){
		print ("enemy collided");
		if (other.transform.tag == "Player") {
			OnContactPlayer ();
		}
	}

	protected void OnCollisionStay(Collision other){
		touchingObject = true;
	}

	public color GetColor(){
		return enemyColor;
	}

	public void ResetStunTimer(){
		stunTimer = maxStunTime;
	}

	public bool IsStunned(){
		return stunTimer > 0.0f;
	}

	protected virtual void MoveOnGround(){
		LookAtPlayer ();
		rigidbody.AddForce(transform.forward*movementForce, ForceMode.Acceleration);

		//limit movement speed
		if(rigidbody.velocity.magnitude > movementSpeed){
			rigidbody.velocity = Vector3.Normalize(rigidbody.velocity) * movementSpeed;
		}
	}

	protected virtual void MoveInAir(){
		MoveOnGround (); //by default move the same way if in air or not in air
	}

	protected virtual void LookAtPlayer(){
		transform.LookAt(playerGO.transform);//attack player//TODO needed this fix to quit hitting ground 100% of time
	}

	protected virtual bool PlayerInRange(){
		return Vector3.Distance (playerGO.transform.position, transform.position) <= attackRange;
	}

	protected virtual bool IsOnGround(){
		Vector3 down = transform.up * -1;
		return Physics.Raycast (transform.position, down, 0.2f) && touchingObject;
	}

	protected virtual void OnContactPlayer(){
		audio.PlayOneShot (damagePlayerNoise);
		print ("Hurting player!");
		Player.health -= damage;
	}


}
