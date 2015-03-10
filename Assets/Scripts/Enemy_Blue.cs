using UnityEngine;
using System.Collections;

public class Enemy_Blue : Enemy {
	
	// Update is called once per frame
	protected override void MoveOnGround () {
		base.MoveOnGround ();
<<<<<<< HEAD
		print ("blue moving");
=======
>>>>>>> master
		rigidbody.AddForce(transform.forward * movementForce, ForceMode.Acceleration);
	}
}
