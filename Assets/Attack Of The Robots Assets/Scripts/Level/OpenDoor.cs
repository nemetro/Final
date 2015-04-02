using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {
	public float dist;
	public float speed;
	
	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 moveTowardsPos;
	
	
	void Start(){
		startPos = moveTowardsPos = transform.position;
		endPos = startPos + Vector3.up * dist; 
	}
	
	void FixedUpdate(){
		if (Vector3.Distance (transform.position, moveTowardsPos) < 0.1f) {
			transform.position = moveTowardsPos;
		} else {
			transform.position = Vector3.MoveTowards (transform.position, moveTowardsPos, speed * Time.fixedDeltaTime);
		}
	}

	public void ToggleTheDoor(){
		if (moveTowardsPos == startPos) {
			moveTowardsPos = endPos;
		} else {
			moveTowardsPos = startPos;
		}
	}
}
