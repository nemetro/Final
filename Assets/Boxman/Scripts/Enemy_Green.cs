using UnityEngine;
using System.Collections;

public class Enemy_Green : Enemy {
	public float jumpForce = 100;
	public float jumpWaitTime = 1.0f;//every 1 seconds enemy can jump
	public float airDashWaitTime = 3.0f;//every 3 seconds enemy can dash
	protected float jumpTimer = 0; 
	protected float airDashTimer = 0;
	
	protected override void FixedUpdate(){
		base.FixedUpdate ();
		
		if (jumpTimer > 0) {
			jumpTimer -= Time.deltaTime;
		}

		print ("green is on ground: " + IsOnGround ());
	}
	
	protected override void MoveOnGround(){
		print ("green ground move");
		if(jumpTimer <= 0){
			transform.LookAt (playerGO.transform.position + new Vector3(0, 1, 0));//look slightly above the player's position
			rigidbody.AddForce(transform.up * jumpForce/2.0f + transform.forward * movementForce/2.0f, ForceMode.Impulse);
			jumpTimer = jumpWaitTime;
		}
	}
}
