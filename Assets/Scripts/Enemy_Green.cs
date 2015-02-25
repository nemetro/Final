using UnityEngine;
using System.Collections;

public class Enemy_Green : Enemy {
	public float jumpWaitTime = 1.0f;//every 1 seconds enemy can jump
	public float airDashWaitTime = 3.0f;//every 3 seconds enemy can dash
	protected bool canJump = false;
	protected float jumpTimer = 0; 
	protected float airDashTimer = 0f;
	
	protected override void Update(){
		base.Update ();
		
		if (jumpTimer > 0) {
			jumpTimer -= Time.deltaTime;
		}
		
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
	}
}
