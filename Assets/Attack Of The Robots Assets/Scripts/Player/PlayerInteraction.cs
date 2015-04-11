using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {
	public InputDevice controller;
	public Camera playerCamera;

	private Text onScreen;


	// Use this for initialization
	void Start () {
		if (controller == null)
		{
			controller = InputManager.Devices[0];
		}
		playerCamera = GetComponent<Camera> ();

	}

	void Awake(){
		onScreen = GameObject.Find ("Hints").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//Open door
		LayerMask environmentMask = LayerMask.GetMask ("Environment");
		RaycastHit interactableHit;
		
		bool interactKeyDown = controller.Action1.WasPressed;
		
		if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out interactableHit, 2.0f, environmentMask)) {
			if(interactableHit.transform.tag == InGameTags.door){
				onScreen.text = "Press 'E' To Use Door";
				if (interactKeyDown) {
					interactableHit.transform.GetComponent<OpenDoor> ().ToggleTheDoor ();
				}
			} else if (interactableHit.transform.tag == InGameTags.deactivateSwitch) { 
				onScreen.text = "Press 'E' To Deactivate Laser Door";
				if (interactKeyDown) {
					GameObject player = this.gameObject;
					interactableHit.transform.GetComponent<LaserDoorSwitchDeactivate>().TurnOffLaserDoor (player);
				}
			}
		} else {
			onScreen.text = "";
		}
	}
}
