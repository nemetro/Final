using UnityEngine;
using System.Collections;

public class Enemy_Blue : Enemy {
	
	// Update is called once per frame
	protected override void MoveOnGround () {
		base.MoveOnGround ();
		print ("blue moving");
		rigidbody.AddForce(transform.forward * movementForce, ForceMode.Acceleration);
	}
}
