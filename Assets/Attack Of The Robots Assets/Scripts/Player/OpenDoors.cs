using UnityEngine;
using System.Collections;

public class OpenDoors : MonoBehaviour {
	//public AnimationClip openDoor;

	private bool doorClosed;
	private Animation anim;

	// Use this for initialization
	void Start () {
		doorClosed = true;
		anim = GetComponentInParent<Animation> ();
	}

	public void ToggleTheDoor(){
		if (!GetComponentInParent<Animation> ().IsPlaying ("DoorOpenAnimation")) {
			return;
		}

		if (doorClosed) {
			doorClosed = false;
			GetComponentInParent<Animation> ().Play ("DoorOpenAnimation");
			print ("Open Door");
		} else {
			doorClosed = true;
			GetComponentInParent<Animation>().Play("DoorCloseAnimation");
			print ("Close Door");
		}
	}
}
