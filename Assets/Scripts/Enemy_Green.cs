using UnityEngine;
using System.Collections;

public class Enemy_Green : Enemy {
<<<<<<< HEAD
	public float jumpWaitTime = 1.0f;//every 1 seconds enemy can jump
	public float airDashWaitTime = 3.0f;//every 3 seconds enemy can dash
	protected bool canJump = false;
	protected float jumpTimer = 0; 
	protected float airDashTimer = 0f;
	
	protected override void Update(){
		base.Update ();
=======
	public float jumpForce = 100;
	public float jumpWaitTime = 1.0f;//every 1 seconds enemy can jump
	public float airDashWaitTime = 3.0f;//every 3 seconds enemy can dash
	protected float jumpTimer = 0; 
	protected float airDashTimer = 0;
	
	protected override void FixedUpdate(){
		base.FixedUpdate ();
>>>>>>> master
		
		if (jumpTimer > 0) {
			jumpTimer -= Time.deltaTime;
		}
<<<<<<< HEAD
		
		if (airDashTimer > 0) {
			airDashTimer -= Time.deltaTime;
		}
		
		canJump = false;
	}
	
	protected override void MoveOnGround(){
		if(canJump && jumpTimer <= 0){
			rigidbody.AddForce(transform.up * movementForce * 100, ForceMode.Impulse);
			jumpTimer = jumpWaitTime;
		}
		
		transform.up = new Vector3(0, 1, 0);
	}
	
	protected override void MoveInAir(){
		//can only dash if not hitting something
		if (canJump == false && airDashTimer <= 0) {
			LookAtPlayer ();
			rigidbody.AddForce (transform.forward * movementForce * 25, ForceMode.Impulse);
			airDashTimer = airDashWaitTime;
		}
	}
	
	void OnCollisionStay(Collision other){
		canJump = true;
=======

		print ("green is on ground: " + IsOnGround ());
	}
	
	protected override void MoveOnGround(){
		print ("green ground move");
		if(jumpTimer <= 0){
			transform.LookAt (playerGO.transform.position + new Vector3(0, 1, 0));//look slightly above the player's position
			rigidbody.AddForce(transform.up * jumpForce/2.0f + transform.forward * movementForce/2.0f, ForceMode.Impulse);
			jumpTimer = jumpWaitTime;
		}
>>>>>>> master
	}
}
