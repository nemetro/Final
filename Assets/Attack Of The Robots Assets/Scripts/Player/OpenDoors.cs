using UnityEngine;
using System.Collections;

public class OpenDoors : MonoBehaviour {
	//public AnimationClip openDoor;

	public bool doorClosed;
	public Animation anim;

	// Use this for initialization
	void Start () {
		doorClosed = true;
		anim = GetComponentInParent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerStay(Collider other) {
		if(doorClosed) {
			if(Input.GetKeyDown ("e") && !GetComponentInParent<Animation>().IsPlaying("DoorOpenAnimation")) {
				doorClosed = false;
				GetComponentInParent<Animation>().Play("DoorOpenAnimation");
				print("Open Door");

			}
		}
		else if(!doorClosed) {
			if(Input.GetKeyDown("e") && !GetComponentInParent<Animation>().IsPlaying("DoorOpenAnimation")) {
				doorClosed = true;
				GetComponentInParent<Animation>().Play("DoorCloseAnimation");
			}
		}
	}
}
